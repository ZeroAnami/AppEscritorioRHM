using AppEscritorioRHM.Core.Application;
using AppEscritorioRHM.Core.Entities;
using AppEscritorioRHM.Core.Interfaces.Infrastructure;
using AppEscritorioRHM.Core.Interfaces.Services;
using AppEscritorioRHM.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppEscritorioRHM.Infrastructure.Data
{
    public class UserProfileHandle : IUserProfileHandle
    {
        UserProfile? _actualUserProfile = null;
        private readonly IUserConnecction _userConnectionService;
        private readonly IEcommerceConnectionValidator _connectionValidator;
        private readonly IEcommerceServiceManager _ecommerceServiceManager;

        public UserProfileHandle(
            IUserConnecction userConnectionService,
            IEcommerceConnectionValidator connectionValidator,
            IEcommerceServiceManager ecommerceServiceManager)
        {
            _userConnectionService = userConnectionService;
            _connectionValidator = connectionValidator;
            _ecommerceServiceManager = ecommerceServiceManager;
        }

        public async Task<ContextResult> LoginAsync(string username, string password)
        {
            var result = await _userConnectionService.GetUserAsync(username, password);
            if (result.Result.Success)
                _actualUserProfile = result.User;
            return result.Result;
        }

        public bool IsLoggedIn() => _actualUserProfile != null;

        public void Logout()
        {
            _actualUserProfile = null;
            _ecommerceServiceManager.Clear();
        }

        public async Task<ContextResult> ValidatePassword(string password)
        {
            if (_actualUserProfile == null) return new ContextResult { Success = false, ErrorMessage = "Usuario no autenticado." };
            return await _userConnectionService.VerifyCredentialsAsync(_actualUserProfile.UserName, password);
        }

        public async Task<ContextResult> AddProjectAsync(ProjectConfiguration project, string password)
        {
            if (!IsLoggedIn()) return NotLoggedInResult();

            if (_actualUserProfile!.ProjectsConfigured.Any(x => x.ProjectId == project.ProjectId))            
                return new ContextResult
                {
                    Success = false,
                    ErrorMessage = "Ya existe un proyecto con el mismo ID."
                };

            var result = await _userConnectionService.AddProject(_actualUserProfile!, project, password);

            if (result.Success)
                _actualUserProfile.ProjectsConfigured.Add(project);

            return result;
        }

        public async Task<ContextResult> UpdateProjectAsync(ProjectConfiguration project, string password)
        {
            if (!IsLoggedIn()) return NotLoggedInResult();

            if (!_actualUserProfile!.ProjectsConfigured.Any(x => x.ProjectId == project.ProjectId))
                return new ContextResult
                {
                    Success = false,
                    ErrorMessage = "No se encontró el proyecto."
                };

            var result = await _userConnectionService.UpdateProject(_actualUserProfile!, project, password);

            if (result.Success) {
                int freshIndex = _actualUserProfile.ProjectsConfigured.FindIndex(x => x.ProjectId == project.ProjectId);
                if (freshIndex != -1)
                    _actualUserProfile.ProjectsConfigured[freshIndex] = project;
                
                // Si es el proyecto activo, reconfigurar el servicio
                if (_actualUserProfile.ProjectSelected == project.ProjectId)
                    await ReconfigureEcommerceServiceAsync();
            }

            return result;
        }

        public async Task<ContextResult> RemoveProjectAsync(string projectId, string password)
        {
            if (!IsLoggedIn()) return NotLoggedInResult();

            ProjectConfiguration? projectToRemove = _actualUserProfile!.ProjectsConfigured
                .FirstOrDefault(x => x.ProjectId == projectId);

            if (projectToRemove == null)
                return new ContextResult
                {
                    Success = false,
                    ErrorMessage = "No se encontró el proyecto a borrar."
                };

            var result = await _userConnectionService.RemoveProjectAsync(_actualUserProfile!, projectId, password);

            if (result.Success)
            {
                _actualUserProfile.ProjectsConfigured.Remove(projectToRemove);
                
                // Si era el proyecto activo, limpiar el servicio
                if (_actualUserProfile.ProjectSelected == projectId)
                {
                    await SetProjectSelectedAsync(string.Empty);
                }
            }

            return result;
        }

        public List<ProjectConfiguration> GetProjects() =>
            _actualUserProfile != null ? _actualUserProfile.ProjectsConfigured : [];

        public ProjectConfiguration? GetProjectSelected()
        {
            if (_actualUserProfile == null) return null;
            if (string.IsNullOrEmpty(_actualUserProfile.ProjectSelected)) return null;
            return _actualUserProfile.ProjectsConfigured
                .FirstOrDefault(x => x.ProjectId == _actualUserProfile.ProjectSelected);
        }

        public string GetUserName() =>
            _actualUserProfile != null ? _actualUserProfile.UserName : string.Empty;

        public async Task<ContextResult> RegisterUserAsync(string username, string password) =>
            await _userConnectionService.RegisterUserAsync(username, password);

        public async Task<ContextResult> RemoveUserAsync(string username, string password) =>
            await _userConnectionService.RemoveUserAsync(username, password);

        public async Task<ContextResult> SetProjectSelectedAsync(string projectId)
        {
            if (!IsLoggedIn()) return NotLoggedInResult();

            _actualUserProfile!.ProjectSelected = projectId;

            // Reconfigurar el servicio de ecommerce para el nuevo proyecto
            var configResult = await ReconfigureEcommerceServiceAsync();
            if (!configResult.Success)
                return configResult;

            // Persistir la selección en el backend
            try
            {
                await _userConnectionService.SetProjectSelected(_actualUserProfile!, projectId);
            }
            catch (Exception ex)
            {
                // No revertimos la selección local, pero informamos del fallo de persistencia
                return new ContextResult
                {
                    Success = true,
                    ErrorMessage = $"Proyecto seleccionado localmente, pero falló la sincronización: {ex.Message}"
                };
            }

            return new ContextResult { Success = true };
        }

        public async Task<ContextResult> TestConnectionWithEcommerce()
        {
            var project = GetProjectSelected();
            if (!IsLoggedIn()) return NotLoggedInResult();
            if (project == null)
                return new ContextResult
                {
                    Success = false,
                    ErrorMessage = "No se ha seleccionado ningún proyecto."
                };

            var ecommerce = Ecommerces.GetAllSoportedEcommerces()
                .FirstOrDefault(x => x.Id == project.EcommerceIdSelected);

            if (ecommerce == null)
                return new ContextResult
                {
                    Success = false,
                    ErrorMessage = "No se encontró la plataforma de ecommerce."
                };

            // Validar todas las conexiones
            var result = await _connectionValidator.ValidateAllEndpointsAsync(project);
            
            // Si la validación es exitosa, configurar el servicio de ecommerce
            if (result.Success)
                _ecommerceServiceManager.ConfigureForProject(project);

            return result;
        }

        /// <summary>
        /// Obtiene el IProductService del proyecto activo.
        /// </summary>
        public IProductService GetProductService()
        {
            if (!IsLoggedIn())
                throw new InvalidOperationException("El usuario no está logeado.");
            
            return _ecommerceServiceManager.GetProductService();
        }

        /// <summary>
        /// Reconfigura el servicio de ecommerce con el proyecto actualmente seleccionado.
        /// </summary>
        private async Task<ContextResult> ReconfigureEcommerceServiceAsync()
        {
            var project = GetProjectSelected();
            if (project == null)
            {
                _ecommerceServiceManager.Clear();
                return new ContextResult { Success = true };
            }

            var ecommerce = Ecommerces.GetAllSoportedEcommerces()
                .FirstOrDefault(x => x.Id == project.EcommerceIdSelected);

            if (ecommerce == null)
            {
                _ecommerceServiceManager.Clear();
                return new ContextResult { Success = false, ErrorMessage = "Ecommerce no encontrado." };
            }

            _ecommerceServiceManager.ConfigureForProject(project);
            return new ContextResult { Success = true };
        }

        private static ContextResult NotLoggedInResult() =>
            new ContextResult { Success = false, ErrorMessage = "El usuario no está logeado." };
    }
}
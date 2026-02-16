using AppEscritorioRHM.Core.Application;
using AppEscritorioRHM.Core.Entities;
using AppEscritorioRHM.Core.Interfaces.Infrastructure;
using AppEscritorioRHM.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppEscritorioRHM.Core.Services
{
    public class EcommerceConnectionValidator : IEcommerceConnectionValidator
    {
        public async Task<ContextResult> ValidateEndpointAsync(
            Endpoints endpoint, 
            ProjectConfiguration project)
        {
            
            try
            {
                IEcommercePlatform ecommerce = GetEcommercePlatformFromProject(project);
                using var client = EcommerceBase.CreateConfiguredHttpClient(endpoint, project);
                var checker = ecommerce.GetCheckConnection(endpoint, client);
                
                if (checker == null)
                    return new ContextResult 
                    { 
                        Success = false, 
                        ErrorMessage = $"No se pudo crear el validador para '{endpoint.EndpointName}'." 
                    };

                bool ok = await checker.CheckConnectionAsync();
                
                return ok 
                    ? new ContextResult { Success = true }
                    : new ContextResult 
                    { 
                        Success = false, 
                        ErrorMessage = $"Error al conectar con '{endpoint.EndpointName}'." 
                    };
            }
            catch (Exception ex)
            {
                return new ContextResult 
                { 
                    Success = false, 
                    ErrorMessage = $"Excepción en '{endpoint.EndpointName}': {ex.Message}" 
                };
            }
        }

        public async Task<ContextResult> ValidateAllEndpointsAsync(
            ProjectConfiguration project)
        {
            IEcommercePlatform ecommerce = GetEcommercePlatformFromProject(project);
            var endpoints = ecommerce.GetConnections();

            foreach (var endpoint in endpoints)
            {
                var result = await ValidateEndpointAsync(endpoint, project);
                if (!result.Success)
                    return result; // Retornar el primer error encontrado
            }

            return new ContextResult { Success = true };
        }

        private static IEcommercePlatform GetEcommercePlatformFromProject(ProjectConfiguration project)
        {
            return Ecommerces.GetAllSoportedEcommerces()
                            .FirstOrDefault(e => e.Id == project.EcommerceIdSelected)
                            ?? throw new InvalidOperationException($"Ecommerce '{project.EcommerceIdSelected}' no soportado.");
        }
    }
}
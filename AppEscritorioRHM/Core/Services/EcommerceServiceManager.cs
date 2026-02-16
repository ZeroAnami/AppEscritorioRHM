using AppEscritorioRHM.Core.Application;
using AppEscritorioRHM.Core.Entities;
using AppEscritorioRHM.Core.Interfaces.Infrastructure;
using AppEscritorioRHM.Core.Interfaces.Services;
using System;

namespace AppEscritorioRHM.Core.Services
{
    public class EcommerceServiceManager : IEcommerceServiceManager
    {
        private readonly IServiceProvider _serviceProvider;
        private IProductService? _currentProductService;
        private IDisposable? _disposableResources;

        public bool IsConfigured => _currentProductService != null;

        public EcommerceServiceManager(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void ConfigureForProject(ProjectConfiguration project)
        {
            var supportedEcommerce = Ecommerces.GetAllSoportedEcommerces().FirstOrDefault(e => e.Id == project.EcommerceIdSelected) 
                ?? throw new InvalidOperationException(
                    $"La plataforma ecommerce '{project.EcommerceIdSelected}' no es soportada.");

            // Limpiar servicio anterior
            Clear();

            // Crear nuevo servicio
            _currentProductService = supportedEcommerce.CreateProductService(project, _serviceProvider);

            // Si el servicio implementa IDisposable, guardarlo para dispose posterior
            if (_currentProductService is IDisposable disposable)
                _disposableResources = disposable;
        }

        public IProductService GetProductService()
        {
            if (_currentProductService == null)
                throw new InvalidOperationException(
                    "No se ha configurado ningún servicio de ecommerce. " +
                    "Llame a ConfigureForProject primero.");

            return _currentProductService;
        }

        public void Clear()
        {
            _disposableResources?.Dispose();
            _disposableResources = null;
            _currentProductService = null;
        }

        public void Dispose() => Clear();        
    }
}
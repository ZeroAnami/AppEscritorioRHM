using AppEscritorioRHM.Core.Entities;
using AppEscritorioRHM.Core.Interfaces.Infrastructure;
using System;

namespace AppEscritorioRHM.Core.Interfaces.Services
{
    /// <summary>
    /// Gestiona el ciclo de vida del IProductService del proyecto activo.
    /// </summary>
    public interface IEcommerceServiceManager : IDisposable
    {
        /// <summary>
        /// Configura el servicio para el proyecto especificado.
        /// Libera el servicio anterior si existía.
        /// </summary>
        void ConfigureForProject(ProjectConfiguration project);

        /// <summary>
        /// Obtiene el IProductService configurado.
        /// Lanza excepción si no se configuró primero.
        /// </summary>
        IProductService GetProductService();

        /// <summary>
        /// Indica si hay un servicio configurado actualmente.
        /// </summary>
        bool IsConfigured { get; }

        /// <summary>
        /// Limpia el servicio actual (al cerrar sesión o cambiar de proyecto).
        /// </summary>
        void Clear();
    }
}
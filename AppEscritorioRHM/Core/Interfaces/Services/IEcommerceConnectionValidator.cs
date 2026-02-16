using AppEscritorioRHM.Core.Entities;
using AppEscritorioRHM.Core.Interfaces.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppEscritorioRHM.Core.Interfaces.Services
{
    public interface IEcommerceConnectionValidator
    {
        /// <summary>
        /// Valida un endpoint específico del proyecto.
        /// </summary>
        Task<ContextResult> ValidateEndpointAsync(
            Endpoints endpoint, 
            ProjectConfiguration project);

        /// <summary>
        /// Valida todos los endpoints del proyecto.
        /// </summary>
        Task<ContextResult> ValidateAllEndpointsAsync(
            ProjectConfiguration project);
    }
}
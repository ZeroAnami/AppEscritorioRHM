using AppEscritorioRHM.Core.Models.Domain;
using AppEscritorioRHM.Core.Models.DTOs.WooCommerce;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppEscritorioRHM.Core.Interfaces
{
    public interface IWPService
    {
        SemaphoreSlim getSemaphore();
        Task<ImageProduct> GetImageByIdAsync(int id, CancellationToken ct = default);
        Task<ImageProduct> DeleteImageByIdAsync(int id, bool force, CancellationToken ct = default);
        Task<bool> CheckConnectionAsync();
    }
}

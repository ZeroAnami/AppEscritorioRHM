using AppEscritorioRHM.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppEscritorioRHM.Core.Interfaces.Infrastructure.Woo
{
    public interface IWPService : ICheckConnection
    {
        SemaphoreSlim getSemaphore();
        Task<ImageProduct> GetImageByIdAsync(int id, CancellationToken ct = default);
        Task<ImageProduct> DeleteImageByIdAsync(int id, bool force, CancellationToken ct = default);
    }
}

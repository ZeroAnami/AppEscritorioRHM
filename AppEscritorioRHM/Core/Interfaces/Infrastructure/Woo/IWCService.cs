using AppEscritorioRHM.Core.Entities;
using AppEscritorioRHM.Infrastructure.ExternalServices.Woo.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppEscritorioRHM.Core.Interfaces.Infrastructure.Woo
{
    public interface IWCService : ICheckConnection
    {
        SemaphoreSlim getSemaphore();
        Task<Product> GetProductByIdAsync(int id, CancellationToken ct = default);
        Task<Product> DeleteProductByIdAsync(int id, bool force, CancellationToken ct = default);
        Task<List<Product>> GetProductsByPageAsync(int page, int perPage = 100, CancellationToken ct = default);
        Task<List<Product>> GetAllProductsAsync(IProgress<ProgressInfo> progress = null, CancellationToken ct = default);
        Task<Category> GetCategoryByIdAsync(int id, CancellationToken ct = default);
        Task<List<ProductVariationsWooDTO>> GetProductVariationsAsync(int parentId, CancellationToken ct = default);
        Task<ProductVariationsWooDTO> GetProductVariationAsync(int parentId, int id, CancellationToken ct = default);
        Task<List<Category>> GetCategoriesAsync(CancellationToken ct = default);
    }
}

using AppEscritorioRHM.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppEscritorioRHM.Core.Interfaces.Infrastructure
{
    public interface IProductService
    {
        Task<List<Product>> GetProductsFromIdsAsync(
            List<int> ids,
            IProgress<ProgressInfo> progress = null,
            CancellationToken ct = default);
        Task<List<ImageProduct>> GetImagesFromIdsAsync(
            List<int> ids,
            IProgress<ProgressInfo> progress = null,
            CancellationToken ct = default);

        Task<List<ImageProduct>> DeleteImagesFromIdsAsync(
            List<int> ids,
            IProgress<ProgressInfo> progress = null,
            CancellationToken ct = default);
        Task<List<Product>> DeleteProductsFromIdsAsync(
            List<int> ids,
            IProgress<ProgressInfo> progress = null,
            CancellationToken ct = default);
        List<int> getIdsFromCsvAsync(string pathToFile);
    }
}

using AppEscritorioRHM.Core.Models.Domain;
using AppEscritorioRHM.Core.Models.DTOs.WooCommerce;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppEscritorioRHM.Core.Interfaces
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

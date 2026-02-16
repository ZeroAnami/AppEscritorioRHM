using AppEscritorioRHM.Core.Entities;
using AppEscritorioRHM.Core.Interfaces.Infrastructure;
using AppEscritorioRHM.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppEscritorioRHM.Core.Application
{
    [Obsolete("Clase temporalmente inutilizada por falta de complejidad, en su lugar usar las funcionalidades de IProductService")]
    public class RedirectionOrchestrator
    {
        private readonly IProductService _productService;
        private readonly IRedirectService _redirectService;

        public RedirectionOrchestrator(
            IProductService productService,
            IRedirectService redirectService)
        {
            _productService = productService;
            _redirectService = redirectService;
        }

        public async Task<List<Product>> GenerateProductsFileAsync(
            string csvFilePath,
            IProgress<ProgressInfo> progress = null,
            CancellationToken ct = default
            )
        {
            var ids = _productService.getIdsFromCsvAsync(csvFilePath);
            var products = await _productService.GetProductsFromIdsAsync(ids, progress, ct);

            if (products.Count == 0)
            {
                throw new Exception("No se han podido procesar productos desde el CSV.");
            }
            return products;
        }
        public string GenerateRedirectsFileAsync(
            List<Product> products
            )
        {
            /**testing(progress);
            return;**/
            string jsonString = _redirectService.GenerateRedirectsFromJsonToCsvAsync(products);
            return jsonString;
        }

        private async void testing(IProgress<string> progress)
        {
            var rd = new Random();
            for (int i = 0; i < 12; i++)
            {
                await Task.Delay(rd.Next(300, 1000));
                progress.Report($"Producto {i + 1} se ha terminado de procesar");
            }
            return;
        }
    }
}

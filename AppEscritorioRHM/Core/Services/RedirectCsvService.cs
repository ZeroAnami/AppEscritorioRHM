using AppEscritorioRHM.Core.Interfaces;
using AppEscritorioRHM.Core.Models.Domain;
using AppEscritorioRHM.Core.Models.DTOs.WooCommerce;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppEscritorioRHM.Core.Services
{
    public class RedirectCsvService : IRedirectService
    {
        private const string Extension = "csv";
        private readonly IBrandMapper _brandMapper;

        public RedirectCsvService(IBrandMapper brandMapper)
        {
            _brandMapper = brandMapper;
        }

        private string CreateCsvRow(Product product)
        {
            var source = product.Url;
            var destination = _brandMapper.MapProductToBrandUrl(product);
            return $"{source},exact,{destination},301,,active,";
        }

        public string GenerateRedirectsJsonAsync(List<Product> products)
        {
            var csvBuilder = new StringBuilder();
            csvBuilder.AppendLine("source,matching,destination,type,category,status,ignore");
            foreach (var product in products)
            {
                csvBuilder.AppendLine(CreateCsvRow(product));
            }

            return csvBuilder.ToString();
        }

        public async Task GenerateRedirectsJsonAsyncDownloaded(List<Product> products, string outputPath)
        {
            string csvString = GenerateRedirectsJsonAsync(products);
            await File.WriteAllTextAsync(outputPath, csvString);
        }

        public string getExtension()
        {
            return Extension;
        }
    }
}

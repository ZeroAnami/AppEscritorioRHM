using AppEscritorioRHM.Core.Interfaces;
using AppEscritorioRHM.Core.Models.Domain;
using AppEscritorioRHM.Core.Models.DTOs.WooCommerce;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppEscritorioRHM.Core.Services
{
    public class RedirectJsonService : IRedirectService
    {
        private readonly IBrandMapper _brandMapper;
        private const string Extension = "json";

        public RedirectJsonService(IBrandMapper brandMapper)
        {
            _brandMapper = brandMapper;
        }

        public JsonRedirect CreateJsonRedirect(Product product)
        {
            var from = product.Url;
            var to = _brandMapper.MapProductToBrandUrl(product);

            return new JsonRedirect(from, to, "1", "301");
        }

        public string GenerateRedirectsJsonAsync(List<Product> products)
        {
            var jsonRedirects = new List<JsonRedirect>();

            foreach (var product in products)
            {
                jsonRedirects.Add(CreateJsonRedirect(product));
            }

            string jsonString = JsonConvert.SerializeObject(jsonRedirects, Formatting.Indented);
            return jsonString;
        }

        public async Task GenerateRedirectsJsonAsyncDownloaded(List<Product> products, string outputPath)
        {
            string jsonString = GenerateRedirectsJsonAsync(products);
            await File.WriteAllTextAsync(outputPath, jsonString);
        }
        public string getExtension()
        {
            return Extension;
        }
    }
}

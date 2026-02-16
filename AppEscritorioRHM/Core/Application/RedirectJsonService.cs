using AppEscritorioRHM.Core.Entities;
using AppEscritorioRHM.Core.Interfaces.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppEscritorioRHM.Core.Application
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

        public string GenerateRedirectsFromJsonToCsvAsync(List<Product> products)
        {
            var jsonRedirects = new List<JsonRedirect>();

            foreach (var product in products)
            {
                jsonRedirects.Add(CreateJsonRedirect(product));
            }

            string jsonString = JsonConvert.SerializeObject(jsonRedirects, Formatting.Indented);
            return jsonString;
        }

        public async Task GenerateCsvFileRedirectsJsonAsyncDownloaded(List<Product> products, string outputPath)
        {
            string jsonString = GenerateRedirectsFromJsonToCsvAsync(products);
            await File.WriteAllTextAsync(outputPath, jsonString);
        }
        public string GetExtension()
        {
            return Extension;
        }
    }
}

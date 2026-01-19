using AppEscritorioRHM.Core.Models.DTOs.WooCommerce;
using AppEscritorioRHM.Core.Models.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppEscritorioRHM.Core.Interfaces
{
    public interface IRedirectService
    {
        Task GenerateRedirectsJsonAsyncDownloaded(List<Product> products, string outputPath);
        string GenerateRedirectsJsonAsync(List<Product> products);
        string getExtension();
    }
}

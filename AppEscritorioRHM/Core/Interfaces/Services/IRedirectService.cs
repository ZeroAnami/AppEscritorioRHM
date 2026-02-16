using AppEscritorioRHM.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppEscritorioRHM.Core.Interfaces.Services
{
    public interface IRedirectService
    {
        Task GenerateCsvFileRedirectsJsonAsyncDownloaded(List<Product> products, string outputPath);
        string GenerateRedirectsFromJsonToCsvAsync(List<Product> products);
        string GetExtension();
    }
}

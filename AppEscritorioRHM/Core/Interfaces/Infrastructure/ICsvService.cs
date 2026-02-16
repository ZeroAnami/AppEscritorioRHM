using System;
using System.Collections.Generic;
using System.Text;

namespace AppEscritorioRHM.Core.Interfaces.Infrastructure
{
    [Obsolete]
    public interface ICsvService
    {
        Task<List<string>> ImportProductsAsync(string filePath);
    }
}

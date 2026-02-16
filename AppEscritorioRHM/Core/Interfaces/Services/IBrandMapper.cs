using AppEscritorioRHM.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppEscritorioRHM.Core.Interfaces.Services
{
    public interface IBrandMapper
    {
        string? GetBrandSlug(string brandName);
        string MapProductToBrandUrl(Product product);
    }
}

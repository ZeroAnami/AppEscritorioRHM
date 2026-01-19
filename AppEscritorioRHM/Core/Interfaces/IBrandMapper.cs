using AppEscritorioRHM.Core.Models.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppEscritorioRHM.Core.Interfaces
{
    public interface IBrandMapper
    {
        string? GetBrandSlug(string brandName);
        string MapProductToBrandUrl(Product product);
    }
}

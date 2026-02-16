using AppEscritorioRHM.Core.Entities;
using AppEscritorioRHM.Core.Interfaces.Services;
using AppEscritorioRHM.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppEscritorioRHM.Core.Application
{
    //TODO: Completar el servicio de mapeo de marcas, las marcas deben cargarse automáticamente, si es posible
    public class BrandMapperService : IBrandMapper
    {
        private readonly AppSettings _settings;

        public BrandMapperService(AppSettings settings)
        {
            _settings = settings;
        }

        public string? GetBrandSlug(string brandName)
        {
            return _settings.MarcaSlug.TryGetValue(brandName, out var slug)
                ? slug
                : null;
        }

        public string MapProductToBrandUrl(Product product)
        {
            if (product.Categories == null || !product.Categories.Any())
            {
                throw new Exception($"El producto {product.Name} (ID: {product.Id}) no tiene categorías");
            }

            var categories = string.Join(" ", product.Categories.Select(c => c.Name));
            var baseUrl = _settings.DominioWeb + "/catalogos/";

            foreach (var brand in _settings.MarcaSlug)
            {
                if (categories.Contains(brand.Key))
                {
                    return baseUrl + brand.Value + "/";
                }
            }

            // TODO: Loggear productos sin marca
            return _settings.DominioWeb + "/";
        }
    }
}

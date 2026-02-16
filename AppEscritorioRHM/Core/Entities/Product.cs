using System;
using System.Collections.Generic;
using System.Text;

namespace AppEscritorioRHM.Core.Entities
{
    /// <summary>
    /// Nucleo del producto de AppEscritorioRHM
    /// </summary>
    public class Product
    {
        public string? Id { get; set; }
        public string? Sku { get; set; }
        public string? Name { get; set; }
        public decimal? RegularPrice { get; set; }
        private decimal? _price;
        public decimal? Price
        {
            get => _price;
            set => _price = value < 0 ? 0 : value;
        }

        public decimal? SalePrice { get; set; }
        public DateTime? Date_created_gmt { get; set; }
        public DateTime? Date_modified_gmt { get; set; }
        public int? Stock { get; set; }
        public string? Url { get; set; }
        public string? ShortDescription { get; set; }
        public string? Description { get; set; }
        public bool? IsVisible { get; set; }
        public List<int>? Variations { get; set; }
        public List<Category>? Categories { get; set; }
        public List<ImageProduct>? Images { get; set; }
        public List<AttributeProduct>? Attributes { get; set; }
    }

    public class ProductVariation
    {
        public string? Id { get; set; }
        public string? Sku { get; set; }
    }

    public class ImageProduct
    {
        public string? Id { get; set; }
        public DateTime? Date_created_gmt { get; set; }
        public DateTime? Date_modified_gmt { get; set; }
        public string? Src { get; set; }
        public string? Name { get; set; }
        public string? Alt { get; set; }
        
    }

    public class Category
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Slug { get; set; }
    }

    public class AttributeProduct
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public List<string>? Options;
        public bool? Variation { get; set; } // Indica si el cliente de la tienda lo puede seleccionar como opción
        public int? Position { get; set; }
    }
}

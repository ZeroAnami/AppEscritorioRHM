using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AppEscritorioRHM.Infrastructure.ExternalServices.Woo.DTOs
{
    public class ProductWooDTO
    {
        public int? id { get; set; }
        // Nombre del producto
        public string? name { get; set; }
        // URL amigable del producto
        public string? slug { get; set; }
        // URL del producto
        public string? permalink { get; set; }
        // Fecha en la que se creó el producto
        public DateTime? date_created { get; set; }
        // Fecha en la que se creó el producto, en formato GMT
        public DateTime? date_created_gmt { get; set; }
        // Fecha en la que se modificó el producto
        public DateTime? date_modified { get; set; }
        // Fecha en la que se modificó el producto, en formato GMT
        public DateTime? date_modified_gmt { get; set; }
        // Tipo del producto
        public string? type { get; set; }
        // Estado del producto
        public string? status { get; set; }
        // Indica si el producto está destacado
        public bool? featured { get; set; }
        // Visibilidad en el catálogo
        public string? catalog_visibility { get; set; }
        // Descripción del producto
        public string? description { get; set; }
        // Breve descripción del producto
        public string? short_description { get; set; }
        // SKU del producto
        public string? sku { get; set; }
        // Precio del producto
        public string? price { get; set; }
        // Precio habitual del producto
        public string? regular_price { get; set; }
        // Precio de venta del producto
        public string? sale_price { get; set; }
        // Fecha en la que inicia la venta del producto
        public DateTime? date_on_sale_from { get; set; }
        // Fecha en la que inicia la venta del producto, en formato GMT
        public DateTime? date_on_sale_from_gmt { get; set; }
        // Fecha en la que termina la venta del producto
        public DateTime? date_on_sale_to { get; set; }
        // Fecha en la que termina la venta del producto, en formato GMT
        public DateTime? date_on_sale_to_gmt { get; set; }
        
        // Indica si el producto está en oferta
        public bool? on_sale { get; set; }
        // Indica si el producto se puede comprar
        public bool? purchasable { get; set; }
        // Total de ventas del producto
        public int? total_sales { get; set; }
        // Indica si el producto se puede descargar
        [JsonProperty("virtual")]
        public bool? IsVirtual { get; set; }
        public bool? downloadable { get; set; }
        // Descargas asociadas al producto
        public List<DownloadWooDTO>? downloads { get; set; }
        // Límite de descargas para el producto
        public int? download_limit { get; set; }
        // Días hasta que la descarga del producto expire
        public int? download_expiry { get; set; }
        // URL externa del producto
        public string? external_url { get; set; }
        // Texto del botón del producto
        public string? button_text { get; set; }
        // Estado de impuestos del producto
        public string? tax_status { get; set; }
        // Clase de impuestos del producto
        public string? tax_class { get; set; }
        // Indica si se está gestionando el stock del producto
        public bool? manage_stock { get; set; }
        // Cantidad de stock del producto
        public int? stock_quantity { get; set; }
        
        // Indica si se permiten pedidos en reserva
        public string? backorders { get; set; }
        // Indica si se permiten pedidos en reserva
        public bool? backorders_allowed { get; set; }
        // Indica si el producto está reservado
        public bool? backordered { get; set; }
        // Indica si el producto se vende individualmente
        public int? low_stock_amount { get; set; }
        public bool? sold_individually { get; set; }
        // Peso del producto
        public string? weight { get; set; }
        // Dimensiones del producto
        public DimensionsWooDTO? dimensions { get; set; }
        // Indica si se requiere envío
        public bool? shipping_required { get; set; }
        // Indica si el envío es tributable
        public bool? shipping_taxable { get; set; }
        // Clase de envío del producto
        public string? shipping_class { get; set; }
        // ID de la clase de envío del producto
        public int? shipping_class_id { get; set; }
        // Indica si se permiten comentarios en el producto
        public bool? reviews_allowed { get; set; }
        // Calificación promedio del producto
        public string? average_rating { get; set; }
        // Conteo de calificaciones del producto
        public int? rating_count { get; set; }        
        // ID de productos para venta ascendente
        public List<int>? upsell_ids { get; set; }
        // ID de productos para venta cruzada
        public List<int>? cross_sell_ids { get; set; }
        // ID del producto padre
        public int? parent_id { get; set; }
        // Nota de compra del producto
        public string? purchase_note { get; set; }
        // Categorías del producto
        public List<CategoryWooDTO>? categories { get; set; }
        // Etiquetas del producto
        public List<TagWooDTO>? tags { get; set; }
        // Imágenes del producto
        public List<ImageWPWooDTO>? images { get; set; }
        // Atributos del producto
        public List<AttributeWooDTO>? attributes { get; set; }
        // Atributos por defecto del producto
        public List<DefaultAttributesWooDTO>? default_attributes { get; set; }
        // Variaciones del producto
        public List<int>? variations { get; set; }
        // Productos agrupados por este producto
        public List<int>? grouped_products { get; set; }
        // Orden de menú del producto
        public int? menu_order { get; set; }
        // Metadatos del producto        
        public string? price_html { get; set; }
        // Representación HTML del precio del producto        
        public List<int>? related_ids { get; set; } 
        // ID de productos relacionados
        //public List<MetaData> meta_data { get; set; }
        // Enlaces relacionados al producto        
        public string? stock_status { get; set; }
        // Estado de stock del producto
        public bool? has_options { get; set; }
        public List<BrandWooDTO>? brands { get; set; }
        public LinksWooDTO? _links { get; set; }
    }

    public class ProductPutWooDTO
    {
        public string name { get; set; }
        // URL amigable del producto
        public string slug { get; set; }
        // Tipo del producto
        public string type { get; set; }
        // Estado del producto
        public string status { get; set; }
        // Indica si el producto está destacado
        public bool? featured { get; set; }
        // Visibilidad en el catálogo
        public string? catalog_visibility;
        // Descripción del producto
        public string? description;
        // Breve descripción del producto
        public string? short_description;
        // SKU del producto
        public string? sku;
        // Precio del producto
        public string? regular_price;
        // Precio de venta del producto
        public string? sale_price { get; set; }
        // Fecha en la que inicia la venta del producto
        public DateTime? date_on_sale_from { get; set; }
        // Fecha en la que inicia la venta del producto, en formato GMT
        public DateTime? date_on_sale_from_gmt { get; set; }
        // Fecha en la que termina la venta del producto
        public DateTime? date_on_sale_to { get; set; }
        // Fecha en la que termina la venta del producto, en formato GMT
        public DateTime? date_on_sale_to_gmt { get; set; }
        // Indica si el producto se puede descargar
        [JsonProperty("virtual")]
        public bool? IsVirtual { get; set; }
        public bool? downloadable { get; set; }
        // Descargas asociadas al producto
        public List<DownloadWooDTO> downloads { get; set; }
        // Límite de descargas para el producto
        public int? download_limit { get; set; }
        // Días hasta que la descarga del producto expire
        public int? download_expiry { get; set; }
        // URL externa del producto
        public string? external_url { get; set; }
        // Texto del botón del producto
        public string? button_text { get; set; }
        // Estado de impuestos del producto
        public string? tax_status { get; set; }
        // Clase de impuestos del producto
        public string? tax_class { get; set; }
        // Indica si se está gestionando el stock del producto
        public bool? manage_stock { get; set; }
        // Cantidad de stock del producto
        public int? stock_quantity { get; set; }
        // Indica si se permiten pedidos en reserva
        public string backorders { get; set; }
        // Indica si se permiten pedidos en reserva
        public int? low_stock_amount { get; set; }
        public bool? sold_individually { get; set; }
        // Peso del producto
        public string weight { get; set; }
        // Dimensiones del producto
        public DimensionsWooDTO? dimensions { get; set; }
        // Clase de envío del producto
        public string? shipping_class { get; set; }
        // Indica si se permiten comentarios en el producto
        public bool? reviews_allowed { get; set; }

        // ID de productos para venta ascendente
        public List<int>? upsell_ids { get; set; }
        // ID de productos para venta cruzada
        public List<int>? cross_sell_ids { get; set; }
        // ID del producto padre
        public int? parent_id { get; set; }
        // Nota de compra del producto
        public string? purchase_note { get; set; }
        // Categorías del producto
        public List<CategoryPutWooDTO>? categories { get; set; }
        // Etiquetas del producto
        public List<TagPutWooDTO>? tags { get; set; }
        // Imágenes del producto
        public List<ImagePutWooDTO>? images { get; set; }
        // Atributos del producto
        public List<AttributeWooDTO>? attributes { get; set; }
        // Atributos por defecto del producto
        public List<DefaultAttributesWooDTO>? default_attributes { get; set; }
        // Productos agrupados por este producto
        public List<int>? grouped_products { get; set; }
        // Orden de menú del producto
        public int? menu_order { get; set; }
        // Metadatos del producto
        //public List<MetaDataPut> meta_data;
        // Enlaces relacionados al producto        
        public string? stock_status { get; set; }
        // Estado de stock del producto
        public bool? has_options { get; set; }
        //public List<Brand> brands = new List<Brand>();
        public LinksWooDTO? _links { get; set; }
    }

    public class ProductPostWooDTO
    {
        public string name;
        // URL amigable del producto
        //public string slug;

        // Tipo del producto
        public string type;
        // Estado del producto
        //public string status;
        // Visibilidad en el catálogo
        public string description = "";
        // Breve descripción del producto
        public string short_description = "";
        // SKU del producto
        public string sku;
        // Precio del producto
        public string regular_price = "";
        // Precio de venta del producto
        public string sale_price = "";
        public string tax_status = "none";
        // Indica si se está gestionando el stock del producto
        public bool manage_stock = false;
        // Cantidad de stock del producto
        public int? stock_quantity = null;
        // Indica si se permiten pedidos en reserva
        public string weight = "0";

        // Categorías del producto
        public List<CategoryPutWooDTO> categories = new List<CategoryPutWooDTO>();
        // Etiquetas del producto
        public List<TagPutWooDTO> tags = new List<TagPutWooDTO>();
        // Imágenes del producto
        public List<ImagePostWooDTO> images = new List<ImagePostWooDTO>();
        // Atributos del producto
        public List<AttributePostWooDTO> attributes = new List<AttributePostWooDTO>();
        // Atributos por defecto del producto
        public List<DefaultAttributesWooDTO> default_attributes = new List<DefaultAttributesWooDTO>();
        // Metadatos del producto
        //public List<MetaDataPut> meta_data;
        // Enlaces relacionados al producto        
        public string stock_status = "instock";
        public string status = "draft";
        //public string status = "publish";

        // Estado de stock del producto}
        //public List<BrandPost> brands = new List<BrandPost>();
    }

    public class ProductPost2WooDTO
    {
        public string name;
        // URL amigable del producto
        //public string slug;

        // Tipo del producto
        public string type;
        // Estado del producto
        //public string status;
        // Visibilidad en el catálogo
        public string description = "";
        // Breve descripción del producto
        public string short_description = "";
        // SKU del producto
        public string sku;
        // Precio del producto
        public string regular_price = "";
        // Precio de venta del producto
        public string sale_price = "";
        public string tax_status = "none";
        // Indica si se está gestionando el stock del producto
        public bool manage_stock = false;
        // Cantidad de stock del producto
        public int? stock_quantity = null;
        // Indica si se permiten pedidos en reserva
        public string weight = "0";

        // Categorías del producto
        public List<CategoryPutWooDTO> categories = new List<CategoryPutWooDTO>();
        // Etiquetas del producto
        public List<TagPutWooDTO> tags = new List<TagPutWooDTO>();
        // Imágenes del producto
        public List<ImagePostWooDTO> images = new List<ImagePostWooDTO>();
        // Atributos del producto
        public List<AttributePostWooDTO> attributes = new List<AttributePostWooDTO>();
        // Atributos por defecto del producto
        public List<DefaultAttributesWooDTO> default_attributes = new List<DefaultAttributesWooDTO>();
        // Metadatos del producto
        //public List<MetaDataPut> meta_data;
        // Enlaces relacionados al producto        
        public string stock_status = "instock";
        // Estado de stock del producto}
        //public List<BrandPost> brands = new List<BrandPost>();
    }

    public class DimensionsWooDTO
    {
        public string length;
        public string width;
        public string height;
    }

    public class CategoryWooDTO
    {
        public int id;
        public string name;
        public string slug;
    }


    public class CategoryPutWooDTO
    {
        public int id;
    }

    public class ImageWPWooDTO
    {
        public int id;
        public DateTime date_created;
        public DateTime date_created_gmt;
        public DateTime date_modified;
        public DateTime date_modified_gmt;
        public string src;
        public string name;
        public string alt;
    }

    public class ImagePutWooDTO
    {
        public int id;
        public string src;
        public string name;
        public string alt;
    }
    public class ImagePostWooDTO
    {
        public int id;
    }

    public class AttributeWooDTO : AttributePostWooDTO
    {        
        public int position;        
    }

    public class AttributePostWooDTO
    {

        public int id { get; private set; }
        public string name { get; private set; }

        public bool visible = true;
        public bool variation = true;
        public List<string> options;
    }

    public class AttributePostOtroWooDTO
    {
        private string name;

        public int id;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
            }
        }

        public bool visible = true;
        public bool variation = true;
        public List<string> options;

        
    }


    public class LinksWooDTO
    {
        public List<LinkWooDTO> self;
        public List<LinkWooDTO> collection;
    }

    public class LinkWooDTO
    {
        public string href;
    }

    public class MetaDataWooDTO
    {
        public int id;
        public string key;
        public string value;
    }

    public class MetaDataPutWooDTO
    {       
        public string key;
        public string value;
    }

    public class TagWooDTO
    {
        public int id;
        public string name;
        public string slug;
    }

    public class TagPutWooDTO
    {
        public int id;
    }

    public class DownloadWooDTO
    {
        public string id;
        public string name;
        public string file;
    }    
    public class DefaultAttributesWooDTO
    {
        protected string Name;

        public int id { get; private set; }

        public string name
        {
            get { return Name; }
            set
            {
                Name = value;
                setId();
            }
        }
        public void setId()
        {
            Dictionary<string, int> nameToId = new Dictionary<string, int>
            {
                {"calidad", 17},
                {"cojín", 9},
                {"color", 2},
                {"confección", 13},
                {"diseño", 16},
                {"fleco", 12},
                {"hechuras", 15},
                {"largo", 10},
                {"medida", 1},
                {"pieza/s", 8},
                {"piezas", 11},
                {"relleno", 7},
                {"borlas", 18}
            };

            id = nameToId[name.ToLower()];
        }

        public string option;
    }
    public class BrandWooDTO
    {
        public int id;
        public string name;
        public string slug;
    }

    public class BrandPostWooDTO
    {
        private static Dictionary<string, int> nameToId = new Dictionary<string, int>
        {
            {"confecciones paula", 1063},
            {"cañete", 1062}
        };

        public BrandPostWooDTO(int id)
        {
            this.id = id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">Nombre de la marca</param>
        /// <exception cref="System.ArgumentNullException">El nombre no se encuentra en la lista de marcas</exception>
        public BrandPostWooDTO(string name)
		{
            id = nameToId[name.ToLower()];
        }

		public int id { get; private set; }

    }

    public class ProductMetaWooDTO
    {
        public List<MetaDataWooDTO> meta_data;
        // Enlaces relacionados al producto
    }
    [Obsolete("Clase no utilizada")]
    public class ProductCSVWooDTO
    {
        public string modelo;
        public string confeccion;
        public string tamano;
        public string color;
        public string precio;
        public string url;
        public string ean;
        public string relleno;
        public string tejido;
        public string composicion;
        public string catalogo;
        public string piezas;
        public string peso;
        public string napa;
        public string marca;
        public string info;
        public string idPadre = "";
        public string confCortina;
        public string precioRebajado;
    }

    public class idAtributosWooDTO
    {
        
    }
    
/// <summary>
/// Clase auxiliar que agrupa un producto "padre" con sus variaciones.
/// Aviso: esta clase es únicamente auxiliar, no se utiliza para importaciones o exportaciones.
/// </summary>
public class ProductPadreWoo
{
    public ProductWooDTO padre;
    public List<ProductVariationsWooDTO> variaciones;

    public ProductPadreWoo(ProductWooDTO padre, List<ProductVariationsWooDTO> variaciones)
    {
        this.padre = padre;
        this.variaciones = variaciones;
    }
}
}

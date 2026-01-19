using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AppEscritorioRHM.Core.Models.DTOs.WooCommerce
{
    public class ProductVariationsInventoryWooDTO
    {
        public string stock_status;
    }
    public class ProductVariationsWooDTO
    {
        // El ID único de la variación del producto.
        public int id;

        // Fecha de creación de la variación del producto.
        public DateTime? date_created;

        // Fecha de creación de la variación del producto en el formato GMT.
        public DateTime? date_created_gmt;

        // Fecha de última modificación de la variación del producto.
        public DateTime? date_modified;

        // Fecha de última modificación de la variación del producto en el formato GMT.
        public DateTime? date_modified_gmt;

        // Descripción de la variación del producto.
        public string description;

        // Enlace permanente o URL de la variación del producto.
        public string permalink;

        // Código único de la variación del producto (SKU).
        public string sku;

        // Precio de la variación del producto.
        public string price;

        // Precio regular de la variación del producto antes de descuentos.
        public string regular_price;

        // Precio de venta de la variación del producto.
        public string sale_price;

        // Fecha de inicio de la venta.
        public DateTime? date_on_sale_from;

        // Fecha de inicio de la venta en el formato GMT.
        public DateTime? date_on_sale_from_gmt;

        // Fecha de fin de la venta.
        public DateTime? date_on_sale_to;

        // Fecha de fin de la venta en el formato GMT.
        public DateTime? dateOdate_on_sale_to_gmtnSaleToGmt;

        // Indica si el producto está en venta.
        public bool on_sale;

        // Estado de la variación del producto (por ejemplo, publicado, borrador, pendiente).
        public string status;
        public bool purchasable;

        [JsonProperty("virtual")]
        public bool IsVirtual;
        public bool downloadable;
        public List<DownloadsVWooDTO> downloads;
        public int download_limit;
        public int download_expiry;
        public string tax_status;
        public string tax_class;
        //public bool manage_stock;
        //public int? stock_quantity;
        public string stock_status;
        public string backorders;
        public bool backorders_allowed;
        public bool backordered;
        public string weight;
        public DimensionsVWooDTO dimensions;
        public string shipping_class;
        public string shipping_class_id;
        public ImageVWooDTO image = new ImageVWooDTO();
        public List<AttributeVWooDTO> attributes;
        public int menu_order;
        public List<Meta_dataWooDTO> meta_data;
        public string ean;
    }

    public class ProductVariationsPutWooDTO
    {        

        // Descripción de la variación del producto.
        public string description;

        // Código único de la variación del producto (SKU).
        public string sku;

        // Precio regular de la variación del producto antes de descuentos.
        public string regular_price;

        // Precio de venta de la variación del producto.
        public string sale_price;

        // Fecha de inicio de la venta.
        public DateTime? date_on_sale_from;

        // Fecha de inicio de la venta en el formato GMT.
        public DateTime? date_on_sale_from_gmt;

        // Fecha de fin de la venta.
        public DateTime? date_on_sale_to;

        // Fecha de fin de la venta en el formato GMT.
        public DateTime? dateOdate_on_sale_to_gmtnSaleToGmt;


        // Estado de la variación del producto (por ejemplo, publicado, borrador, pendiente).
        public string status;

        [JsonProperty("virtual")]
        public bool IsVirtual;
        public bool downloadable;
        public List<DownloadsVWooDTO> downloads = new List<DownloadsVWooDTO>();
        public int download_limit;
        public int download_expiry;
        public string tax_status;
        public string tax_class;
        public bool manage_stock;
        public int? stock_quantity;
        public string stock_status;
        public string backorders;
        public string weight;
        public DimensionsVWooDTO dimensions;
        public string shipping_class;
        public ImageVPostWooDTO image = new ImageVPostWooDTO();
        public List<AttributeVPostWooDTO> attributes = new List<AttributeVPostWooDTO>();
        public int menu_order;
        //public List<string> meta_data;
    }

    public class ProductVariationsPostWooDTO
    {
        // Descripción de la variación del producto.
        //public string description;

        // Código único de la variación del producto (SKU).
        public string sku;

        // Precio regular de la variación del producto antes de descuentos.
        public string regular_price;

        // Precio de venta de la variación del producto.
        public string sale_price;

        // Estado de la variación del producto (por ejemplo, publish, private, draft, pending).
        public string status = "publish";

        public string tax_status = "none";
        public bool manage_stock = false;
        public int? stock_quantity = null;
        public string stock_status = "instock";
        public string weight = "0";
        public ImageVPostWooDTO image = new ImageVPostWooDTO();
        public List<AttributeVPostWooDTO> attributes = new List<AttributeVPostWooDTO>();
        //public List<string> meta_data;

        /*public ProductVariationsPost()
        {

        }

        public ProductVariationsPost(ProductVariations pv)
        {
            sku = pv.sku;
            regular_price = pv.regular_price;
            sale_price = pv.sale_price;
        }*/
    }

    public class DimensionsVWooDTO
    {
        public string length;
        public string width;
        public string height;
    }

    public class ImageVWooDTO
    {
        public int id;
        public DateTime? date_created;
        public DateTime? date_created_gmt;
        public DateTime? date_modified;
        public DateTime? date_modified_gmt;
        public string src;
        public string name;
        public string alt;
    }

    public class ImageVPutWooDTO
    {
        public int id;
        public string src;
        public string name;
        public string alt;
    }
    public class ImageVPostWooDTO
    {
        public int id;
    }

    public class AttributeVWooDTO
    {
        public int id;
        public string name;
        public string option;
        public void setId()
        {
            Dictionary<string, int> nameToId = new Dictionary<string, int>
            {
                {"Calidad", 17},
                {"Cojín", 9},
                {"Color", 2},
                {"Confección", 13},
                {"Diseño", 16},
                {"Fleco", 12},
                {"Hechuras", 15},
                {"Largo", 10},
                {"Medida", 1},
                {"Pieza/s", 8},
                {"Piezas", 11},
                {"Relleno", 7},
                {"Borlas", 18}
            };

            id = nameToId[name];
        }
    }
    public class AttributeVPostWooDTO
    {
        private string name;

        public int id { get; private set; }

        [JsonProperty("name")]
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                setId();
            }
        }
        public string option;

        public void setId()
        {
            Dictionary<string, int> nameToId = new Dictionary<string, int>
            {
                {"Calidad", 17},
                {"Cojín", 9},
                {"Color", 2},
                {"Confección", 13},
                {"Diseño", 16},
                {"Fleco", 12},
                {"Hechuras", 15},
                {"Largo", 10},
                {"Medida", 1},
                {"Pieza/s", 8},
                {"Piezas", 11},
                {"Relleno", 7},
                {"Borlas", 18}
            };

            id = nameToId[name];
        }
    }

    public class DownloadsVWooDTO
    {
        public string id;
        public string name;
        public string file;
    }

    public class ProductVariationsNameWooDTO : ProductVariationsWooDTO
    {
        public string name;
        public int parent_id;
    }
    [JsonConverter(typeof(Meta_dataConverterWooDTO))]
    public class Meta_dataWooDTO
    {
        public int id { get; set; }
        public string key { get; set; }
        public object value { get; set; }
    }

    public class Meta_dataConverterWooDTO : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Meta_dataWooDTO);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            // Este método maneja la deserialización del objeto JSON.
            JObject jsonObject = JObject.Load(reader);
            Meta_dataWooDTO meta = new Meta_dataWooDTO();

            meta.id = (int)jsonObject["id"];
            meta.key = (string)jsonObject["key"];

            var valueToken = jsonObject["value"];
            if (valueToken.Type == JTokenType.String)
            {
                meta.value = (string)valueToken;
            }
            else
            {
                meta.value = valueToken.ToObject<object>();
            }

            return meta;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Meta_dataWooDTO meta = (Meta_dataWooDTO)value;
            JObject obj = new JObject
        {
            { "id", meta.id },
            { "key", meta.key },
            { "value", meta.value is string ? JToken.FromObject(meta.value) : JToken.FromObject(meta.value.ToString()) }
        };

            obj.WriteTo(writer);
        }
    }
}

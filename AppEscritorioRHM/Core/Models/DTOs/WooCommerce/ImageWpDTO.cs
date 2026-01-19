using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace AppEscritorioRHM.Core.Models.DTOs.WooCommerce
{
    /// <summary>
    /// DTO Principal que representa la respuesta completa del adjunto (Imagen)
    /// </summary>
    public class AttachmentWpDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("date")]
        public DateTime? Date { get; set; }

        [JsonProperty("date_gmt")]
        public DateTime? DateGmt { get; set; }

        [JsonProperty("guid")]
        public RenderedFieldWpDTO Guid { get; set; }

        [JsonProperty("modified")]
        public DateTime? Modified { get; set; }

        [JsonProperty("modified_gmt")]
        public DateTime? ModifiedGmt { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("title")]
        public RenderedFieldWpDTO Title { get; set; }

        [JsonProperty("author")]
        public int Author { get; set; }

        [JsonProperty("comment_status")]
        public string CommentStatus { get; set; }

        [JsonProperty("ping_status")]
        public string PingStatus { get; set; }

        [JsonProperty("template")]
        public string Template { get; set; }

        [JsonProperty("meta")]
        public List<object> Meta { get; set; }

        [JsonProperty("class_list")]
        public List<string> ClassList { get; set; }

        [JsonProperty("description")]
        public RenderedFieldWpDTO Description { get; set; }

        [JsonProperty("caption")]
        public RenderedFieldWpDTO Caption { get; set; }

        [JsonProperty("alt_text")]
        public string AltText { get; set; }

        [JsonProperty("media_type")]
        public string MediaType { get; set; }

        [JsonProperty("mime_type")]
        public string MimeType { get; set; }

        [JsonProperty("media_details")]
        public MediaDetailsWpDTO MediaDetails { get; set; }

        [JsonProperty("post")]
        public int? PostParentId { get; set; }

        [JsonProperty("source_url")]
        public string SourceUrl { get; set; }

        [JsonProperty("_links")]
        public LinksDTOWp Links { get; set; }
    }

    // =========================================================
    // CLASES AUXILIARES (Anidadas en el JSON)
    // =========================================================

    /// <summary>
    /// Para campos que vienen con la propiedad "rendered" (Title, Guid, Description)
    /// </summary>
    public class RenderedFieldWpDTO
    {
        [JsonProperty("rendered")]
        public string Rendered { get; set; }
    }

    /// <summary>
    /// Detalles técnicos del archivo multimedia
    /// </summary>
    public class MediaDetailsWpDTO
    {
        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("file")]
        public string File { get; set; }

        [JsonProperty("filesize")]
        public long? Filesize { get; set; }

        /// <summary>
        /// Diccionario clave-valor para soportar tamaños dinámicos 
        /// (ej: "medium", "woocommerce_thumbnail", "1536x1536")
        /// </summary>
        [JsonProperty("sizes")]
        public Dictionary<string, ImageSizeWpDTO> Sizes { get; set; }

        [JsonProperty("image_meta")]
        public ImageMetaWpDTO ImageMeta { get; set; }
    }

    /// <summary>
    /// Representa cada variante de tamaño de la imagen
    /// </summary>
    public class ImageSizeWpDTO
    {
        [JsonProperty("file")]
        public string File { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("filesize")]
        public long? Filesize { get; set; }

        [JsonProperty("mime_type")]
        public string MimeType { get; set; }

        [JsonProperty("source_url")]
        public string SourceUrl { get; set; }

        [JsonProperty("uncropped")]
        public bool? Uncropped { get; set; }
    }

    /// <summary>
    /// Metadatos EXIF de la imagen (Cámara, apertura, etc.)
    /// </summary>
    public class ImageMetaWpDTO
    {
        [JsonProperty("aperture")]
        public string Aperture { get; set; }

        [JsonProperty("credit")]
        public string Credit { get; set; }

        [JsonProperty("camera")]
        public string Camera { get; set; }

        [JsonProperty("caption")]
        public string Caption { get; set; }

        [JsonProperty("created_timestamp")]
        public string CreatedTimestamp { get; set; }

        [JsonProperty("copyright")]
        public string Copyright { get; set; }

        [JsonProperty("focal_length")]
        public string FocalLength { get; set; }

        [JsonProperty("iso")]
        public string Iso { get; set; }

        [JsonProperty("shutter_speed")]
        public string ShutterSpeed { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("orientation")]
        public string Orientation { get; set; }

        // Keywords suele venir vacío, pero es una lista
        [JsonProperty("keywords")]
        public List<string> Keywords { get; set; }
    }

    // =========================================================
    // CLASES PARA LOS LINKS (HATEOAS)
    // =========================================================
    // Opcional: Solo si necesitas navegar por los links "_links"

    public class LinksDTOWp
    {
        [JsonProperty("self")]
        public List<LinkItemDTOWp> Self { get; set; }

        [JsonProperty("collection")]
        public List<LinkItemDTOWp> Collection { get; set; }

        [JsonProperty("about")]
        public List<LinkItemDTOWp> About { get; set; }

        [JsonProperty("author")]
        public List<LinkItemDTOWp> Author { get; set; }

        [JsonProperty("replies")]
        public List<LinkItemDTOWp> Replies { get; set; }
    }

    public class LinkItemDTOWp
    {
        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("embeddable")]
        public bool? Embeddable { get; set; }
    }
}

using AppEscritorioRHM.Core.Application;
using AppEscritorioRHM.Core.Entities;
using AppEscritorioRHM.Core.Interfaces.Infrastructure;
using AppEscritorioRHM.Core.Interfaces.Infrastructure.Woo;
using AppEscritorioRHM.Core.Utilities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppEscritorioRHM.Infrastructure.ExternalServices.Woo
{
    public class WooCommerce : EcommerceBase 
    {
        public enum EndpointOptions
        {
            WooCommerce = 0,
            Wordpress = 1
        }
        public override string Id => "WooCommerce";
        public override string Name => "WooCommerce";

        private static readonly Endpoints WooCommerceEndpoint = new Endpoints
        {
            EndpointId = "WooCommerce",
            EndpointName = "WooCommerce",
            Endpoint = "/wp-json/wc/v3/",
            ImagesMedia = string.Empty,
            Products = "products",
            Categories = "categories",
            Variations = "variations",
            Attributes = "attributes"
        };

        private static readonly Endpoints WordpressEndpoint = new Endpoints
        {
            EndpointId = "Wordpress",
            EndpointName = "Wordpress",
            Endpoint = "/wp-json/wp/v2/",
            ImagesMedia = "media",
            Products = string.Empty,
            Categories = string.Empty,
            Variations = string.Empty,
            Attributes = string.Empty
        };

        public override List<Endpoints> GetConnections() =>
            [
                WooCommerceEndpoint,
                WordpressEndpoint
            ];

        public static Endpoints GetEndpoint(EndpointOptions option)
        {
            return option switch
            {
                EndpointOptions.WooCommerce => WooCommerceEndpoint,
                EndpointOptions.Wordpress => WordpressEndpoint,
                _ => throw new ArgumentOutOfRangeException(nameof(option), option, null)
            };
        }

        public override IProductService CreateProductService(ProjectConfiguration project, IServiceProvider serviceProvider)
        {
            var wcClient = CreateConfiguredHttpClient(WooCommerceEndpoint, project);
            var wpClient = CreateConfiguredHttpClient(WordpressEndpoint, project); 
            return new ProductWooService(new WCService(wcClient), new WPService(wpClient));
        }

        public override ICheckConnection GetCheckConnection(Endpoints endpoints, HttpClient client)
        {
            if (endpoints.EndpointId == WooCommerceEndpoint.EndpointId)
                return new WCService(client);
            if (endpoints.EndpointId == WordpressEndpoint.EndpointId)
                return new WPService(client);
            
            return null;
        }
    }
}

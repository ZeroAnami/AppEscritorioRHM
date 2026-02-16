using AppEscritorioRHM.Core.Interfaces.Infrastructure;
using AppEscritorioRHM.Infrastructure.ExternalServices.Woo;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppEscritorioRHM.Core.Application
{
    public static class Ecommerces
    {
        private static readonly IReadOnlyList<IEcommercePlatform> _soportedEcommerces =
        [
            new WooCommerce()
        ];

        public static IReadOnlyList<IEcommercePlatform> GetAllSoportedEcommerces() => _soportedEcommerces;

    }
}

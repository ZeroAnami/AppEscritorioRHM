using AppEscritorioRHM.Core.Entities;
using AppEscritorioRHM.Core.Interfaces.Infrastructure;
using AppEscritorioRHM.Core.Utilities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace AppEscritorioRHM.Core.Application
{
    public abstract class EcommerceBase : IEcommercePlatform
    {
        public abstract string Id { get; }
        public abstract string Name { get; }
        public abstract List<Endpoints> GetConnections();
        public abstract IProductService CreateProductService(ProjectConfiguration project, IServiceProvider serviceProvider);

        public static HttpClient CreateConfiguredHttpClient(Endpoints endpoint, ProjectConfiguration project)
        {
            var client = new HttpClient();
            var tokens = project.ConnectionsTokens.FirstOrDefault(x => x.EndpointId == endpoint.EndpointId)
                ?? throw new Exception($"No se encontraron las credenciales para el endpoint {endpoint.EndpointId}.");
            ConnectionHelper.ConfigureHttpClient(client, project.Domain, endpoint.Endpoint, tokens.ConsumerKey, tokens.ConsumerSecret);
            return client;
        }
        public abstract ICheckConnection GetCheckConnection(Endpoints endpoints, HttpClient client);
    }
}

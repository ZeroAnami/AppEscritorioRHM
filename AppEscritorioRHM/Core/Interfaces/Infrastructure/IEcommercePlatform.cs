using AppEscritorioRHM.Core.Entities;
using AppEscritorioRHM.Core.Utilities;
using AppEscritorioRHM.Properties;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace AppEscritorioRHM.Core.Interfaces.Infrastructure
{
    public interface IEcommercePlatform
    {
        string Id { get; }
        string Name { get; }
        List<Endpoints> GetConnections();
        IProductService CreateProductService(ProjectConfiguration project, IServiceProvider serviceProvider);

        ICheckConnection GetCheckConnection(Endpoints endpoints, HttpClient client);
    }
}

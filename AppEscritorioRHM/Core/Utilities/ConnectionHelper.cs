using DocumentFormat.OpenXml.Office2010.Excel;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace AppEscritorioRHM.Core.Utilities
{
    public static class ConnectionHelper
    {
        private const int WaitTime = 10;
        public static void ConfigureHttpClient(HttpClient client, string domain, string endpoint, string consumerKey, string consumerSecret)
        {
            client.BaseAddress = new Uri(domain + $"{endpoint}");
            client.Timeout = TimeSpan.FromMinutes(WaitTime);
            if (!string.IsNullOrEmpty(consumerKey))
            {
                var authBytes = Encoding.ASCII.GetBytes($"{consumerKey}:{consumerSecret}");
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authBytes));
            }
        }
    }
}

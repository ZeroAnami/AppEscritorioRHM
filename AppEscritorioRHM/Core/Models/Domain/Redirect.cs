using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppEscritorioRHM.Core.Models.Domain
{
    public class Redirect
    {
        public string from { get; set; }
        public string match { get; set; }
        public string to { get; set; }
        public string status { get; set; }
        public string timestamp { get; set; }
        public string type { get; set; }

        public Redirect(string from, string to, string status = "1", int dominioLength = 0)
        {
            if (string.IsNullOrWhiteSpace(from))
                throw new ArgumentException("El campo 'from' no puede estar vacío.", nameof(from));
            if (string.IsNullOrWhiteSpace(to))
                throw new ArgumentException("El campo 'to' no puede estar vacío.", nameof(to));

            this.from = from.Trim();
            this.to = to.Trim();

            var uri = new Uri(from);
            this.match = uri.AbsolutePath;

            this.status = string.IsNullOrWhiteSpace(status) ? "1" : status.Trim();
            this.timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
            this.type = "redirection";
        }

        [JsonConstructor]
        private Redirect() { }
    }
}

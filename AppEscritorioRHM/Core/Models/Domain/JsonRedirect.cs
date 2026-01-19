using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace AppEscritorioRHM.Core.Models.Domain
{
    public class JsonRedirect
    {
        public Redirect redirect { get; set; }
        public Meta metas { get; set; }

        public JsonRedirect(string from, string to, string status = "1", string redirect_code = "301")
        {
            this.redirect = new Redirect(from, to, status);
            this.metas = new Meta(redirect_code);
        }

        [JsonConstructor]
        private JsonRedirect() { }
    }
}

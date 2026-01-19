using System;
using System.Collections.Generic;
using System.Text;

namespace AppEscritorioRHM.Core.Utilities
{
    public class AppSettings
    {
        public string? DominioWeb { get; set; }
        public Dictionary<string, string> MarcaSlug { get; } = new Dictionary<string, string>
        {
            {"Paula", "confecciones-paula"},
            {"JVR", "jvr"},
            {"Cañete", "canete"},
            {"Eysa", "eysa"},
            {"Reig", "reig-marti"},
            {"Fundeco", "fundeco"},
            {"Antilo", "antilo"},
            {"Sandeco", "sandeco"},
            {"Belnou", "belnou"},
            {"Euromoda", "creaciones-euromoda"},
            {"Karamelo", "karamelo"}
        };
    }
}

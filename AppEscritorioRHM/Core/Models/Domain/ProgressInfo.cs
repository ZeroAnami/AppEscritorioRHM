using System;
using System.Collections.Generic;
using System.Text;

namespace AppEscritorioRHM.Core.Models.Domain
{
    public class ProgressInfo
    {
        public int current { get; set; }
        public int? max { get; set; }
        public int? id { get; set; }
        public ProgressInfo(int current, int? max = null, int? id = null)
        {
            this.current = current;
            this.max = max;
            this.id = id;
        }
    }
}

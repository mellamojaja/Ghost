using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ghost.AI.API.Models
{
    public class GhostGameMove
    {
        public string Word { get; set; }
        public string NextWord { get; set; }
        public bool Finished { get; set; }
        public int Winner { get; set; }
    }
}
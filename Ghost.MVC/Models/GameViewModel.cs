using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ghost.MVC.Models
{
    public class GameViewModel
    {
        public bool HelpPlayer { get; set; }
        public string NewMove { get; set; }
        public List<string> Moves { get; set; }
        public string Explanation { get; set; }
        public PlayerModel Player { get; set; }
    }
}
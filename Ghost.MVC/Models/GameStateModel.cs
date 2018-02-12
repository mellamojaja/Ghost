using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ghost.MVC.Models
{
    public class GameStateModel
    {
        public int CurrentPlayer { get; set; }
        public string Word { get; set; }
        public string Move { get; set; }
        
        public PlayerModel Player { get; set; }
        public GameStateAnalysisModel Analysis { get; set; }        
    }
}
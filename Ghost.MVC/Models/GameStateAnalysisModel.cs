using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ghost.MVC.Models
{
    public class GameStateAnalysisModel
    {
        public int Winner { get; set; }
        public int ExpectedWinner { get; set; }
        public string Explanation { get; set; }
    }
}
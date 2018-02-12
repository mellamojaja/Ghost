using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ghost.MVC.Models
{
    public class GameAnalysisModel
    {
        public int Winner { get; set; }
        public int ExpectedWinner { get; set; }
        public string Explanation { get; set; }
        public string Help { get; set; }

        public GameAnalysisModel()
        {
            Winner = -1;
            ExpectedWinner = -1;
            Explanation = "";
            Help = "";
        }
    }
}
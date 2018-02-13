﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ghost.AI.API.Models
{
    public class GameAnalysisModel
    {
        public bool HasWinner { get; set; }
        public int Winner { get; set; }
        public int ExpectedWinner { get; set; }
        public string Explanation { get; set; }
        public string Help { get; set; }

        public GameAnalysisModel()
        {
            HasWinner = false;
            Winner = -1;
            ExpectedWinner = -1;
            Explanation = "";
            Help = "";
        }
    }
}
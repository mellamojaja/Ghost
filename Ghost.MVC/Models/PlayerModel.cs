﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ghost.MVC.Models
{
    public class PlayerModel
    {
        public string Name { get; set; }
        public int NumberOfGames { get; set; }
        public int NumberOfVictories { get; set; }

        public PlayerModel() { }

        public PlayerModel(string playerName)
        {
            Name = playerName;
            NumberOfGames = 0;
            NumberOfVictories = 0;
        }

        
    }

}
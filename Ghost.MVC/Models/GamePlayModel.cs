using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ghost.MVC.Models
{
    public class GamePlayModel
    {
        [Display(Name = "Show player help")]
        public bool EnablePlayerHelp { get; set; }

        [Display(Name = "New letter")]
        [StringLength(1)]
        [RegularExpression("[a-zA-Z]")]
        public string NewMove { get; set; }

        public List<string> Moves { get; set; }        
        public PlayerModel Player { get; set; } 
        public GameAnalysisModel Analysis { get; set; }

        public GamePlayModel(PlayerModel player, GameAnalysisModel analysis)
        {
            EnablePlayerHelp = false;
            NewMove = "";
            Moves = new List<string>();
            Player = player;
            Analysis = analysis;
        }

        public void Reset()
        {
            Player.NumberOfGames = Player.NumberOfGames + 1;
            Player.NumberOfVictories = Analysis.Winner == 0 ? Player.NumberOfVictories + 1 : Player.NumberOfVictories;

            Analysis = new GameAnalysisModel();
            NewMove = "";
            Moves = new List<string>();
        }
    }
}
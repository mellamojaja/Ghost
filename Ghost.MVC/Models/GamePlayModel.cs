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
        public bool ShowPlayerHelp { get; set; }

        [Display(Name = "New letter")]
        [StringLength(1, ErrorMessage = "Please enter only one character")]
        [RegularExpression("[a-zA-Z]", ErrorMessage = "Please enter a valid letter (from 'a' to 'z')")]
        public string NewMove { get; set; }

        public List<string> Moves { get; set; }        
        public PlayerModel Player { get; set; } 
        public GameAnalysisModel Analysis { get; set; }

        public GamePlayModel(PlayerModel player, GameAnalysisModel analysis)
        {
            ShowPlayerHelp = false;
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

        public string GetCurrentWord()
        {
            var result = "";            
            Moves.ForEach(aMove => result = result + aMove);
            return result;
        }
        public string GetNewWord()
        {
            return GetCurrentWord() + NewMove;
        }
    }
}
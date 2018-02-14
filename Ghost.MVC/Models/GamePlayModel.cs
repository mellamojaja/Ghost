using Ghost.MVC.Models.Dtos;
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
        //[StringLength(1, ErrorMessage = "Please enter only one character")]
        //[RegularExpression("[a-zA-Z]", ErrorMessage = "Please enter a valid letter (from 'a' to 'z')")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "[a-zA-Z]")]
        public string NewMove { get; set; }

        public List<string> Moves { get; set; }
        
        public string Word { get; set; }

        public PlayerModel Player { get; set; }

        public string ComputerLastMove { get; set; }
        public string ComputerLastMoveExplanation { get; set; }  
        
        public string Winner { get; set; }
        public string WinnerExplanation { get; set; }

        #region Constructors
        public GamePlayModel() { }

        public GamePlayModel(PlayerModel player)
        {
            ShowPlayerHelp = false;
            NewMove = "";
            Moves = new List<string>();
            Word = "";
            Player = player;
            ComputerLastMove = "";
            ComputerLastMoveExplanation = "";
            Winner = "";
            WinnerExplanation = "";
        }
        #endregion

        public void Reset(GameAnalysisModel analysis = null)
        {
            Player.NumberOfGames = Player.NumberOfGames + 1;
            if (analysis != null)
            {
                Player.NumberOfVictories = analysis.Winner == 0 ? Player.NumberOfVictories + 1 : Player.NumberOfVictories;
            }                        
            NewMove = "";
            Moves = new List<string>();
            Word = "";
            ComputerLastMove = "";
            ComputerLastMoveExplanation = "";
            Winner = "";
            WinnerExplanation = "";
        }       
    }
}
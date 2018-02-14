using Ghost.MVC.Models;
using Ghost.MVC.Models.Dtos;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace Ghost.MVC.Controllers
{
    public class GameController : Controller
   {       
        public ActionResult Play()
        {
            if (Game == null)
            {
                CreateGame(new PlayerNameModel() { Name = "Human" });
            }

            return View(Game);
        }
        
        [HttpPost]
        public ActionResult Play(GamePlayModel game)
        {
            if (Request.Params["command"] != null && Request.Params["command"].Equals("Pass"))
            {
                return RedirectToAction("Pass", Game);
            }

            RememberInputFieldValues(game);
            
            // Human user move
            ApplyUserMove(game);

            var state = new GameStateModel { Word = Game.Word };
            var analysis = Utilities.Analize(state);

            if (analysis.HasWinner)
            {
                TakeNoteOfWinner(analysis);                
                // End the game
                return RedirectToAction("Results", "EndGame", Game);
            }

            // IA move
            var newState = Utilities.NextMove(state);
            ApplyComputerMove(newState, analysis);

            state = new GameStateModel { Word = Game.Word };
            analysis = Utilities.Analize(state);

            if (analysis.HasWinner)
            {
                TakeNoteOfWinner(analysis);
                // End the game
                return RedirectToAction("Results", "EndGame", Game);
            }

            return View(Game);
        }

        [HttpPost]
        public ActionResult Pass(GamePlayModel game)
        {
            RememberInputFieldValues(game);

            // Not implemented yed
            
            return View(Game);
        }

        // GET: Game/Create
        public ActionResult Create(PlayerNameModel playerName)
        {
            CreateGame(playerName);

            return RedirectToAction("play");
        }

        // GET: Game/Reset
        public ActionResult Reset()
        {
            if (Game == null)
            {
                CreateGame(new PlayerNameModel() { Name = "Human" });
            }
            else
            {
                Game.Reset();
            }            
            return RedirectToAction("play");
        }

        #region Private        
        private GamePlayModel Game
        {
            get { return Session["game"] as GamePlayModel; }
            set { Session["game"] = value; }
        }

        private void CreateGame(PlayerNameModel playerName)
        {            
            var player = new PlayerModel(playerName.Name);
            Game = new GamePlayModel(player);
        }        

        private void RememberInputFieldValues(GamePlayModel game)
        {
            Game.ShowPlayerHelp = game.ShowPlayerHelp;            
            Game.NewMove = game.NewMove;
        }

        private void ApplyUserMove(GamePlayModel game)
        {
            var newMove = game.NewMove.Trim().ToLower();

            if (! string.IsNullOrEmpty(newMove))
            {
                newMove = newMove.Substring(0, 1);
                Game.Moves.Add(newMove);
                Game.Word = Game.Word + newMove;
                Game.NewMove = "";
            }
        }       

        private void ApplyComputerMove(GameStateModel newState, GameAnalysisModel analysis)
        {
            if (Game.Word.Length < newState.Word.Length)
            {
                var lastLetter = newState.Word.Substring(newState.Word.Length - 1, 1);
                Game.Moves.Add(lastLetter);
                Game.Word = newState.Word;
                Game.ComputerLastMove = newState.Word;
                var message = Regex.Replace(analysis.Help, "player 1", (match => "I" ), RegexOptions.IgnoreCase);
                message = Regex.Replace(message, "player 0", (match => "you"), RegexOptions.IgnoreCase);
                Game.ComputerLastMoveExplanation = string.Format("{0}. So, I have played the letter '{1}'", message, lastLetter);
            }            
        }

        private void TakeNoteOfWinner(GameAnalysisModel analysis)
        {
            if (analysis.HasWinner)
            {
                Game.Winner = analysis.Winner == 0 ? Game.Player.Name : "Computer";
                var message = Regex.Replace(analysis.Explanation, "player 1", (match => "Computer"), RegexOptions.IgnoreCase);
                message = Regex.Replace(message, "player 0", (match => Game.Player.Name), RegexOptions.IgnoreCase);
                Game.WinnerExplanation = string.Format("{0}. So, {1} player has won", message, Game.Winner);
            }
        }
        #endregion

    }
}
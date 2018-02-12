using Ghost.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ghost.MVC.Controllers
{
    public class GameController : Controller
    {
        public GameViewModel GameView;
        public GameStateModel GameState;

        // GET: Game
        public ActionResult Play()
        {         
            return View(GameView);
        }

        // GET: Game
        [HttpPost]
        public ActionResult Play(GameViewModel game)
        {
            if (! string.IsNullOrEmpty(game.NewMove))
            {
                // Do the move
            }
            
            return View(GameView);
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
            ResetGame();
            return RedirectToAction("play");
        }

        
        

        private void CreateGame(PlayerNameModel playerName)
        {
            var analysis = new GameStateAnalysisModel() { Winner = -1, ExpectedWinner = -1, Explanation = "Who knows waht will happen..." };
            var player = new PlayerModel() { Name = playerName.Name, NumberOfGames = 0, NumberOfVictories = 0 };

            GameState = new GameStateModel() { CurrentPlayer = 0, Word = "", Move = "", Analysis = analysis, Player = player };
            GameView = new GameViewModel()
            {
                HelpPlayer = false,
                Moves = new List<string>(),
                NewMove = "",
                Explanation = "Who knows waht will happen...",
                Player = player
            };
        }

        private void ResetGame()
        {            
            var player = new PlayerModel()
            {
                Name = GameState.Player.Name,
                NumberOfGames = GameState.Player.NumberOfGames + 1,
                NumberOfVictories = GameState.Analysis.Winner == 0 ? GameState.Player.NumberOfVictories + 1 : GameState.Player.NumberOfVictories
            };

            var analysis = new GameStateAnalysisModel() { Winner = -1, ExpectedWinner = -1, Explanation = "Who knows waht will happen..." };

            GameState = new GameStateModel() { CurrentPlayer = 0, Word = "", Move = "", Analysis = analysis, Player = player };
            GameView = new GameViewModel()
            {
                HelpPlayer = GameView.HelpPlayer,
                Moves = new List<string>(),
                NewMove = "",
                Explanation = "Who knows waht will happen...",
                Player = player
            };
        }

    }
}
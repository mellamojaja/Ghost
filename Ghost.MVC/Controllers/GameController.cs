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
        private GamePlayModel Game {
            get { return Session["game"] as GamePlayModel; }
            set { Session["game"] = value; }
        }

        // GET: Game
        public ActionResult Play()
        {         
            return View(Game);
        }

        // GET: Game
        [HttpPost]
        public ActionResult Play(GamePlayModel game)
        {
            UpdateConfigFields(game);
            if (string.IsNullOrEmpty(game.NewMove))
            {                
                DoNewMove(game);
            }
            
            if (Game.Analysis.Winner != -1)
            {
                return View("GameEnd", Game);
            }

            return View(Game);
        }

        [HttpPost]
        public ActionResult Pass(GamePlayModel game)
        {
            UpdateConfigFields(game);
            Pass(game);           
            return View("GameEnd", Game);
        }

        // GET: Game/Create
        public ActionResult Create(PlayerNameModel playerName)
        {
            CreateGame(playerName);
            return RedirectToAction("play", Game);
        }

        // GET: Game/Reset
        public ActionResult Reset()
        {
            Game.Reset();
            return RedirectToAction("play");
        }

        
        

        private void CreateGame(PlayerNameModel playerName)
        {
            var analysis = new GameAnalysisModel();
            var player = new PlayerModel(playerName.Name);
            Game = new GamePlayModel(player, analysis);
        }        

        private void UpdateConfigFields(GamePlayModel game)
        {
            Game.EnablePlayerHelp = game.EnablePlayerHelp;            
        }

        private void DoNewMove(GamePlayModel game)
        {
            return;
        }

    }
}
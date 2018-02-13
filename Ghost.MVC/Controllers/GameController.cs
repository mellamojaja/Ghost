using Ghost.MVC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Ghost.MVC.Controllers
{
    public class GameController : Controller
    {
        private static HttpClient _client;

        private static HttpClient Client
        {
            get
            {
                if (_client == null)
                {
                    _client = new HttpClient();
                    _client.BaseAddress = new Uri("http://localhost:50519");
                    _client.DefaultRequestHeaders.Accept.Clear();
                    _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                }
                return _client;
            }
        }

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
            Game.ShowPlayerHelp = game.ShowPlayerHelp;            
        }

        private void DoNewMove(GamePlayModel game)
        {
            var path = "localhost:50519/api/GhostAnalyser/" + game.GetNewWord();
            var analysis = await GetAnalysisAsync();
            return;
        }

        static async Task<GameAnalysisModel> GetAnalysisAsync()
        {
            GameAnalysisModel analysis = null;
            HttpResponseMessage response = await Client.GetAsync("api/GhostAnalyser");
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                analysis = JsonConvert.DeserializeObject<GameAnalysisModel>(responseBody);                
            }
            return analysis;
        }
    }
}
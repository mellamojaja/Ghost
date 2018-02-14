using Game.Library;
using Ghost.AI.API.Models;
using System;

namespace Ghost.AI.API.Controllers
{
    internal sealed class Analyzer
    {
        public static Analyzer Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (syncRoot)
                    {
                        if (_instance == null)
                        {
                            _instance = new Analyzer();
                        }
                    }
                }
                return _instance;
            }
        }

        public GameAnalysisModel Analyze(GameStateModel stateModel)
        {
            var state = _game.CreateState(stateModel.Word);
            _game.State = state;
            var analysis = _player.Analyse(_game);
            var analysisModel = new GameAnalysisModel(analysis);

            return analysisModel;
        }

        public GameStateModel NextMove(GameStateModel stateModel)
        {
            var state = _game.CreateState(stateModel.Word);
            _game.State = state;
            var newState = _player.NextMove(_game);
            if (newState == null)
            {
                return stateModel;
            }                
            var newStateModel = new GameStateModel(newState.State);

            return newStateModel;
        }

        #region Private
        private static volatile Analyzer _instance;
        private static object syncRoot = new Object();
        private static IGame _game;
        private static IPlayer _player;

        private Analyzer()
        {
            _game = GameFactory.Instance.CreateGame(GameType.ghost, "whatever");
            _player = _game.CreatePlayer("Player1", PlayerType.perfectIa);
        }
        #endregion
    }
}
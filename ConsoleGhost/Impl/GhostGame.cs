using System;
using System.Collections.Generic;

namespace ConsoleGhost.Impl
{
    public class GhostGame : IGame<GhostGameState>
    {       
        public GhostGame()
        {
            _name = "Optimal Ghost";
            _playerList = new List<IPlayer<IGame<GhostGameState>, GhostGameState>>();
            Reset();
        }

        public string Name { get { return _name; } }

        public GhostGameState State { get { return _state; } }
        

        public IGameResult Result { get { return _result; } }

        public List<IPlayer<IGame<GhostGameState>, GhostGameState>> PlayerList { get { return _playerList; } }
        
        public void AddPlayer(IPlayer<IGame<GhostGameState>, GhostGameState> player)
        {
            _playerList.Add(player);
        }

        public IPlayer<IGame<GhostGameState>, GhostGameState> CreatePlayer(string name, PlayerType playerType)
        {
            //switch (playerType) {
            //    case PlayerType.human:
            //        break;

            //    case PlayerType.ia:
            //        break;

            //    case PlayerType.perfectIa:
            //        break;
            //}
            // jaja
            return new GhostPerfectIAPlayer(name);
        }       

        public void Reset()
        {
            _state = new GhostGameState() { CurrentPlayer = 0, Word = "" };
            _result = new GhostGameStateAnalysis(_state);
        }

        public void Start()
        {
            if (PlayerList.Count != 2)
            {
                throw new Exception(string.Format("To play {0} game, there must be 2 players", Name));
            }
            return; // jaja
        }

        public IPlayer<IGame<GhostGameState>, GhostGameState> NewPlayer(string playerType)
        {
            throw new NotImplementedException();
        }

        #region Private
        private string _name;
        private GhostGameState _state;
        private IGameResult _result;
        private List<IPlayer<IGame<GhostGameState>, GhostGameState>> _playerList;       
        #endregion
    }
}

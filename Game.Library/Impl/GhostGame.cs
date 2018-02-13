using System;
using System.Collections.Generic;

namespace Game.Library.Impl
{
    public class GhostGame : IGame
    {       
        public GhostGame()
        {
            _name = "Optimal Ghost";
            _playerList = new List<IPlayer>();
            _analysisTree = GhostAnalysisTree.Instance;
            Reset();
        }

        public string Name { get { return _name; } }

        public IState State { get { return _state; } set { _state = value as GhostGameState; } }
        

        public IGameResult GetResult()
        {
            return GetAnalysis();
        }

        public IStateAnalysis GetAnalysis()
        {
            var treeNode = _analysisTree.FindWordNodeOrLongestExistingRoot(_state.Word);
            var wordType = _analysisTree.FindWordType(_state.Word);
            var lastPlayer = State.CurrentPlayer == 0 ? 1 : 0;            

            if (wordType == WordType.invalid)
            {
                // Last player lost
                return new GhostGameStateAnalysis(_state)
                {
                    Winner = _state.CurrentPlayer,
                    Explanation = string.Format("Player {0} proposed the word '{1}' which does not exist", lastPlayer, _state.Word),
                    ExpectedWinner = _state.CurrentPlayer,
                    ExpectedMaxTurns = 0
                };
            }

            if (wordType == WordType.derived)
            {
                // The proposed word would be reachable so something went wrong.
                return new GhostGameStateAnalysis(_state)
                {                    
                    Explanation = string.Format("Player {0} proposed the word '{1}' but it won't be reachable because there is shorter word", lastPlayer, _state.Word)                 
                };
            }

            if (wordType == WordType.completed)
            {
                // Last player lost
                return new GhostGameStateAnalysis(_state)
                {
                    Winner = _state.CurrentPlayer,
                    Explanation = string.Format("Player {0} proposed the word '{1}' which exists", lastPlayer, _state.Word),
                    ExpectedWinner = _state.CurrentPlayer,
                    ExpectedMaxTurns = 0
                };
            }

            if (treeNode.Value.ExpectedWinner > -1)
            {
                // A player is about to win
                return new GhostGameStateAnalysis(_state)
                {                    
                    ExpectedWinner = treeNode.Value.ExpectedWinner,                    
                    ExpectedMaxTurns = treeNode.Value.ExpectedMaxTurns,
                    Help = string.Format("Player {0} should win going for the word '{1}'", treeNode.Value.ExpectedWinner, treeNode.Value.RecommendedWordList[0])
                };
            }

            // We don't know yet
            return new GhostGameStateAnalysis(_state)
            {
                Help = string.Format("The result is uncertain... the game could last {0} more turns, for example going for '{1}' or '{2}'",
                    treeNode.Value.LongestPossibleWord.Length - _state.Word.Length,
                    treeNode.Value.ShortestPossibleWord,
                    treeNode.Value.LongestPossibleWord)
            };
        }

        public List<IPlayer> PlayerList { get { return _playerList; } }
        
        public void AddPlayer(IPlayer player)
        {
            _playerList.Add(player);
        }

        public IPlayer CreatePlayer(string name, PlayerType playerType)
        {
            switch (playerType)
            {
                case PlayerType.human:
                    throw new NotImplementedException();                    

                case PlayerType.ia:
                    throw new NotImplementedException();                    

                case PlayerType.perfectIa:
                default:
                    return new GhostPerfectIAPlayer(name);                    
            }                        
        }       

        public void Reset()
        {
            _state = new GhostGameState("");
            _result = new GhostGameStateAnalysis(_state);
        }

        public void Start()
        {
            if (PlayerList.Count != 2)
            {
                throw new Exception(string.Format("To play {0} game, there must be 2 players", Name));
            }
            
            // todo: Add here the logic to call the players in order and get their moves

            return; 
        }        

        #region Private
        private string _name;
        private GhostGameState _state;
        private IGameResult _result;
        private List<IPlayer> _playerList;
        private GhostAnalysisTree _analysisTree;
        #endregion
    }
}

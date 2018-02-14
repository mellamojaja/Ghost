using System;
using System.Collections.Generic;

namespace Game.Library.Impl
{
    internal class GhostGame : IGame
    {       
        public GhostGame(string name)
        {
            _name = name;
            _playerList = new List<IPlayer>();
            _analysisTree = GhostAnalysisTree.Instance;
            Reset();
        }

        public string Name { get { return _name; } }

        public IState State { get { return _state; } set { _state = value as GhostGameState; } }
        

        public IGameResult Result { get { return Analysis; } }        

        public IStateAnalysis Analysis            
        {
            get
            {
                var treeNode = _analysisTree.FindWordNodeOrLongestExistingRoot(_state.Word);
                var wordType = _analysisTree.FindWordType(_state.Word);
                var lastPlayer = State.CurrentPlayer == 0 ? 1 : 0;
                var result = treeNode.Value.Copy();
                result.State = _state;

                switch (wordType)
                {
                    case WordType.invalid:
                        // Last player lost                
                        result.Winner = _state.CurrentPlayer;
                        result.Explanation = string.Format("Player {0} proposed the word '{1}' which does not exist", lastPlayer, _state.Word);
                        result.ExpectedWinner = _state.CurrentPlayer;
                        result.ExpectedMaxTurns = 0;
                        return result;

                    case WordType.derived:
                        // The proposed word would be reachable so something went wrong.
                        result.Explanation = string.Format("Player {0} proposed the word '{1}' but it won't be reachable because there is shorter word", lastPlayer, _state.Word);
                        return result;

                    case WordType.completed:
                        // Last player lost
                        result.Winner = _state.CurrentPlayer;
                        result.Explanation = string.Format("Player {0} proposed the word '{1}' which exists", lastPlayer, _state.Word);
                        result.ExpectedWinner = _state.CurrentPlayer;
                        result.ExpectedMaxTurns = 0;
                        return result;
                }

                if (treeNode.Value.ExpectedWinner > -1)
                {
                    // A player is about to win                
                    var wordList = "";
                    treeNode.Value.RecommendedWordList.ForEach(word => wordList = wordList + (wordList == "" ? "" : ", ") + word);
                    result.Help = string.Format("Player {0} may win going for: {1}", treeNode.Value.ExpectedWinner, wordList);
                    return result;
                }
                else
                {
                    // We don't know yet
                    result.Help = string.Format("The result is uncertain... the game may last {0} more turns, for example going for '{1}' or '{2}'",
                            treeNode.Value.LongestPossibleWord.Length - _state.Word.Length,
                            treeNode.Value.ShortestPossibleWord,
                            treeNode.Value.LongestPossibleWord);
                    return result;
                }
            }         
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
                    return new GhostHumanPlayer(name);

                case PlayerType.ia:
                    return new GhostDumbIAPlayer(name);

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

        public bool HasFinished { get { return Analysis.Winner > -1; } }

        public void PlayNextTurn()
        {
            if (PlayerList.Count != 2)
            {
                throw new Exception(string.Format("To play {0} game, there must be 2 players", Name));
            }

            if (! HasFinished)
            {
                var nextState = PlayerList[State.CurrentPlayer].NextMove(this);
                State = nextState;
            }            
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

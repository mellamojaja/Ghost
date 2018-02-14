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

        public IState State { get; set; }
        

        public IGameResult Result { get { return Analysis; } }        

        public IStateAnalysis Analysis            
        {
            get
            {
                var treeNode = _analysisTree.FindWordNodeOrLongestExistingRoot(State.State);
                var wordType = _analysisTree.FindWordType(State.State);
                var lastPlayer = State.CurrentPlayer == 0 ? 1 : 0;
                var result = treeNode.Value.Copy();
                result.State = State;

                switch (wordType)
                {
                    case WordType.invalid:
                        // Last player lost                
                        result.Winner = State.CurrentPlayer;
                        result.Explanation = string.Format("Player {0} proposed the word '{1}' which does not exist", lastPlayer, State.State);
                        result.ExpectedWinner = State.CurrentPlayer;
                        result.ExpectedMaxTurns = 0;
                        return result;

                    case WordType.derived:
                        // The proposed word would be reachable so something went wrong.
                        result.Explanation = string.Format("Player {0} proposed the word '{1}' but it won't be reachable because there is shorter word", lastPlayer, State.State);
                        return result;

                    case WordType.completed:
                        // Last player lost
                        result.Winner = State.CurrentPlayer;
                        result.Explanation = string.Format("Player {0} proposed the word '{1}' which exists", lastPlayer, State.State);
                        result.ExpectedWinner = State.CurrentPlayer;
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
                            treeNode.Value.LongestPossibleWord.Length - State.State.Length,
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
            State = CreateState("");
            _result = new GhostGameStateAnalysis(State);
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

        public IState CreateState(object stateData)
        {
            var word = stateData as string;
            var currentPlayer = word.Length % 2 == 0 ? 0 : 1;
            return new GameState(currentPlayer, word, word);
        }

        #region Private
        private string _name;        
        private IGameResult _result;
        private List<IPlayer> _playerList;
        private GhostAnalysisTree _analysisTree;       
        #endregion
    }
}

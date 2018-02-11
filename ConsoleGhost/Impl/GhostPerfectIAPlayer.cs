using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleGhost.Impl
{
    public class GhostPerfectIAPlayer : IPlayer<IGame<GhostGameState>, GhostGameState>
    {
        public GhostPerfectIAPlayer(string name)
        {
            _name = name;
            _type = PlayerType.perfectIa;
            _rnd = new Random();           
        }

        public string Name => _name;

        public PlayerType Type => _type;

        public GhostGameState NextMove(IGame<GhostGameState> game)
        {
            var result = Analyse(game);

            if (result.Winner > -1)
            {
                // Someone has already won. No more moves. 
                return null;
            }

            var treeNode = AnalysisTree.FindWordNodeOrLongestExistingRoot(game.State.Word);
            var nextPlayer = game.State.CurrentPlayer == 0 ? 1 : 0;
            List<TreeNode<GhostGameStateAnalysis>> nodeList;
            if (result.ExpectedWinner == game.State.CurrentPlayer) 
            {
                // Move to win
                nodeList = treeNode.Children.Where(node => node.Value.ExpectedWinner == game.State.CurrentPlayer).ToList();             
            } 
            else
            {
                // Move to extend the game
                nodeList = treeNode.Children.Where(node => node.Value.LongestPossibleWord == treeNode.Value.LongestPossibleWord).ToList();                
            }
            var desiredState = PickRandom(nodeList).Value.State;
            return new GhostGameState()
            {
                CurrentPlayer = nextPlayer,
                Word = desiredState.Word
            };        
        }

        public IStateAnalysis Analyse(IGame<GhostGameState> game)
        {
            var treeNode = AnalysisTree.FindWordNodeOrLongestExistingRoot(game.State.Word);
            var wordType = AnalysisTree.FindWordType(game.State.Word);
            var lastPlayer = game.State.CurrentPlayer == 0 ? 1 : 0;

            if (wordType == WordType.invalid)
            {                            
                return new GhostGameStateAnalysis()
                {                    
                    Winner = game.State.CurrentPlayer,
                    Explanation = string.Format("Player {0} has proposed the word '{1}' which is not valid", lastPlayer, game.State.Word)
                };
            }

            if (wordType == WordType.derived)
            {
                return new GhostGameStateAnalysis()
                {
                    Winner = -1,
                    Explanation = string.Format("Player {0} has proposed the word '{1}' but it won't be reachable because there is shorter word", lastPlayer, game.State.Word)
                };
            }

            if (wordType == WordType.completed)
            {                
                return new GhostGameStateAnalysis()
                {
                    Winner = game.State.CurrentPlayer,
                    Explanation = string.Format("Player {0} has completed the word '{1}'", lastPlayer, game.State.Word)
                };
            }

            if (treeNode.Value.ExpectedWinner > -1)
            {
                // A player is about to win
                return new GhostGameStateAnalysis()
                {
                    Winner = -1,
                    Explanation = string.Format("Player {0} has a lot of chances to win going for '{1}' word", treeNode.Value.ExpectedWinner, treeNode.Value.ShortestPossibleWord),
                    ExpectedWinner = treeNode.Value.ExpectedWinner
                };
            }

            // We don't know yet
            return new GhostGameStateAnalysis()
            {
                Winner = -1,
                Explanation = string.Format("The result is uncertain... the game could last {0} more turns, for example going for '{1}' or '{2}'",
                    treeNode.Value.LongestPossibleWord.Length - game.State.Word.Length, treeNode.Value.ShortestPossibleWord, treeNode.Value.LongestPossibleWord)
            };
        }

        #region Protected
        protected string _name;
        protected PlayerType _type;
        protected Random _rnd;
        protected GhostAnalysisTree _analysisTree;
        protected GhostAnalysisTree AnalysisTree {
            get
            {
                if (_analysisTree == null)
                {
                    _analysisTree = GhostAnalysisTree.Instance;
                }
                return _analysisTree;
            }
        }        
        
        protected TreeNode<GhostGameStateAnalysis> PickRandom(List<TreeNode<GhostGameStateAnalysis>> nodeList)
        {
            var r = _rnd.Next(nodeList.Count);
            return nodeList[r];
        }
        
        #endregion
    }
}

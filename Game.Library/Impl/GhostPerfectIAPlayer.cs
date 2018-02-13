using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Library.Impl
{
    public class GhostPerfectIAPlayer : IPlayer
    {
        public GhostPerfectIAPlayer(string name)
        {
            _name = name;
            _type = PlayerType.perfectIa;
            _rnd = new Random();           
        }

        public string Name => _name;

        public PlayerType Type => _type;

        public IState NextMove(IGame game)
        {
            var analyse = Analyse(game) as GhostGameStateAnalysis;
            var state = game.State as GhostGameState;

            if (analyse.Winner > -1)
            {
                // Someone has already won. No more moves. 
                return null;
            }

            var recommendedWord = PickRandom(analyse.RecommendedWordList);
            var result = recommendedWord.Substring(0, state.Word.Length + 1);

            return new GhostGameState(result);
        }

        public IStateAnalysis Analyse(IGame game)
        {
            return game.GetAnalysis();
        }

        #region Private and Protected
        protected string _name;
        protected PlayerType _type;
        protected Random _rnd;

        private GhostAnalysisTree _analysisTree;
        private GhostAnalysisTree AnalysisTree {
            get
            {
                if (_analysisTree == null)
                {
                    _analysisTree = GhostAnalysisTree.Instance;
                }
                return _analysisTree;
            }
        }        
        
        private string PickRandom(List<string> wordList)
        {
            var r = _rnd.Next(wordList.Count);
            return wordList[r];
        }               
        #endregion
    }
}

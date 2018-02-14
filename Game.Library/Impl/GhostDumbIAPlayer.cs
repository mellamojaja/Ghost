using System.Linq;

namespace Game.Library.Impl
{
    internal class GhostDumbIAPlayer : GhostBasePlayer, IPlayer
    {
        public GhostDumbIAPlayer(string name) : base(name)
        {            
            _type = PlayerType.ia;
        }
       
        public override IState NextMove(IGame game)
        {
            var analyse = Analyse(game) as GhostGameStateAnalysis;
            var state = game.State as GhostGameState;

            if (analyse.Winner > -1)
            {
                // Someone has already won. No more moves. 
                return null;
            }

            var treeNode = GhostAnalysisTree.Instance.FindWordNodeOrLongestExistingRoot(state.Word);
            var wordList = treeNode.Children.Select(child => (child.Value.State as GhostGameState).Word ).ToList();

            var recommendedWord = PickRandom(wordList);
            var result = recommendedWord.Substring(0, state.Word.Length + 1);

            return new GhostGameState(result);
        }         
    }
}

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
            var state = game.State;

            if (analyse.Winner > -1)
            {
                // Someone has already won. No more moves. 
                return null;
            }

            var treeNode = GhostAnalysisTree.Instance.FindWordNodeOrLongestExistingRoot(state.State);
            var wordList = treeNode.Children.Select(child => child.Value.State.State).ToList();

            var recommendedWord = PickRandom(wordList);
            var result = recommendedWord.Substring(0, state.State.Length + 1);

            return game.CreateState(result);
        }         
    }
}

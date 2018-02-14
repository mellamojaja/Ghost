namespace Game.Library.Impl
{
    internal class GhostPerfectIAPlayer : GhostBasePlayer, IPlayer
    {
        public GhostPerfectIAPlayer(string name) : base(name)
        {            
            _type = PlayerType.perfectIa;           
        }
        
        public override IState NextMove(IGame game)
        {
            var analyse = Analyse(game) as GhostGameStateAnalysis;
            var state = game.State as GameState;

            if (analyse.Winner > -1)
            {
                // Someone has already won. No more moves. 
                return null;
            }

            var recommendedWord = PickRandom(analyse.RecommendedWordList);
            var result = recommendedWord.Substring(0, state.State.Length + 1);

            return game.CreateState(result);
        }        
    }
}

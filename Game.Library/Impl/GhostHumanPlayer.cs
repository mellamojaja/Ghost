using System;

namespace Game.Library.Impl
{
    internal class GhostHumanPlayer : GhostBasePlayer, IPlayer
    {
        public GhostHumanPlayer(string name) : base(name)
        {            
            _type = PlayerType.human;
        }

        public override IState NextMove(IGame game)
        {
            var line = Console.ReadLine();
            var newWord = game.State.State + (line.ToLower().TrimStart())[0];
            return game.CreateState(newWord);
        }          
    }
}

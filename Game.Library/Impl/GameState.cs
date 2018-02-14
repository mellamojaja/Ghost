using System.Diagnostics;

namespace Game.Library.Impl
{
    [DebuggerDisplay("P{CurrentPlayer} '{StateDescription}'")]
    public class GameState : IState
    {        
        public GameState(int currentPlayer, string state, string description) 
        {            
            CurrentPlayer = currentPlayer;
            State = state;
            Description = description;
        }
                
        public int CurrentPlayer { get; set; }
        
        public string State { get; set; }

        public string Description { get; set; } 
    }
}

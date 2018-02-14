using System.Diagnostics;

namespace Game.Library.Impl
{
    [DebuggerDisplay("P{CurrentPlayer} '{StateDescription}'")]
    internal class GhostGameState : IState
    {        
        public GhostGameState(string word) 
        {
            CurrentPlayer = word.Length % 2 == 0 ? 0 : 1;
            Word = word;
        }
                
        public int CurrentPlayer { get; set; }

        /// <summary>
        /// A generic alias of "Word" to fullfil the IState interface
        /// </summary>
        public object State { get { return Word; } set { Word = value as string; } }

        public string StateDescription { get { return Word; } }

        /// <summary>
        /// The word so far
        /// </summary>
        public string Word { get; set; }
    }
}

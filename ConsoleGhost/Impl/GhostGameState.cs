using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGhost.Impl
{
    [DebuggerDisplay("P{CurrentPlayer} '{Word}'")]
    public class GhostGameState
    {
        /// <summary>
        /// The index of the player (starting from 0) that has to play now
        /// </summary>
        public int CurrentPlayer { get; set; }

        /// <summary>
        /// The word so far
        /// </summary>
        public string Word { get; set; }
    }
}

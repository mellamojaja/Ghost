using System.Diagnostics;

namespace ConsoleGhost.Impl
{
    [DebuggerDisplay("P{State.CurrentPlayer} '{State.Word}' W{Winner} EW{ExpectedWinner} From '{ShortestPossibleWord}' to '{LongestPossibleWord}'")]
    public class GhostGameStateAnalysis : IStateAnalysis
    {
        public GhostGameStateAnalysis() { }

        public GhostGameStateAnalysis(GhostGameState state)
        {
            State = state;
            Winner = -1;
            ExpectedWinner = -1;
        }
        
        public GhostGameState State { get; set; }

        public int Winner { get; set; }
        public string Explanation { get; set; }

        public int ExpectedWinner { get; set; }
        public string Help { get; set; }

        public string LongestPossibleWord { get; set; }
        public string ShortestPossibleWord { get; set; }
    }
}

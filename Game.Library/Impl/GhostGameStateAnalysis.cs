using System.Collections.Generic;
using System.Diagnostics;

namespace Game.Library.Impl
{
    [DebuggerDisplay("P{State.CurrentPlayer} '{State.StateDescription}' W{Winner} EW{ExpectedWinner} From '{ShortestPossibleWord}' to '{LongestPossibleWord}'")]
    internal class GhostGameStateAnalysis : IStateAnalysis
    {                
        public GhostGameStateAnalysis(IState state)
        {
            State = state;
            Winner = -1;
            ExpectedWinner = -1;
            ExpectedMaxTurns = -1;
            Explanation = "";
            Help = "";
            LongestPossibleWord = "";
            ShortestPossibleWord = "";
            RecommendedWordList = new List<string>();
        }

        public IState State { get; set; } 

        public int Winner { get; set; }
        public string Explanation { get; set; }

        public int ExpectedWinner { get; set; }
        public int ExpectedMaxTurns { get; set; }
        public string Help { get; set; }

        public string LongestPossibleWord { get; set; }
        public string ShortestPossibleWord { get; set; }
        public List<string> RecommendedWordList { get; set; }

        public GhostGameStateAnalysis Copy()
        {
            var newList = new List<string>(RecommendedWordList);

            var result = new GhostGameStateAnalysis(State)
            {
                State = this.State,
                Winner = Winner,
                ExpectedWinner = ExpectedWinner,
                ExpectedMaxTurns = ExpectedMaxTurns,
                Explanation = Explanation,
                Help = Help,
                LongestPossibleWord = LongestPossibleWord,
                ShortestPossibleWord = ShortestPossibleWord,
                RecommendedWordList = newList
            };

            return result;
        }                   
    }
}

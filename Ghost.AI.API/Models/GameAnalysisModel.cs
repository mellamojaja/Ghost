using Game.Library;

namespace Ghost.AI.API.Models
{
    public class GameAnalysisModel
    {
        public bool HasWinner { get; set; }
        public int Winner { get; set; }
        public int ExpectedWinner { get; set; }
        public int ExpectedMaxTurns { get; set; }
        public string Explanation { get; set; }
        public string Help { get; set; }

        public GameAnalysisModel()
        {
            HasWinner = false;
            Winner = -1;
            ExpectedWinner = -1;
            ExpectedMaxTurns = -1;
            Explanation = "";
            Help = "";
        }

        public GameAnalysisModel(IStateAnalysis analysis)
        {
            HasWinner = analysis.Winner > -1;
            Winner = analysis.Winner;
            ExpectedWinner = analysis.ExpectedWinner;
            ExpectedMaxTurns = analysis.ExpectedMaxTurns;
            Explanation = analysis.Explanation;
            Help = analysis.Help;
        }
    }
}
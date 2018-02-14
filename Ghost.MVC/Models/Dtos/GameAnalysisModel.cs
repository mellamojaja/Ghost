namespace Ghost.MVC.Models.Dtos
{
    public class GameAnalysisModel
    {
        public bool HasWinner { get; set; }
        public int Winner { get; set; }
        public int ExpectedWinner { get; set; }
        public int ExpectedMaxTurns { get; set; }
        public string Explanation { get; set; }
        public string Help { get; set; }       
    }
}
namespace Game.Library
{
    /// <summary>
    /// This class represents the analysis of a "state" or "turn" of the game. 
    /// </summary>
    public interface IStateAnalysis : IGameResult
    {
        /// <summary>
        /// Index of winning player (starting on 0) or -1 if the winner is unknown
        /// </summary>
        int ExpectedWinner { get; set; }

        /// <summary>
        /// The max amount of turns where one player will win; or -1 if there is no "ExpectedWinner" yet
        /// </summary>
        int ExpectedMaxTurns { get; set; }

        /// <summary>
        /// Some guidance of how to play this turn
        /// </summary>
        string Help { get; set; }
    }
}

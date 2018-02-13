namespace Game.Library
{
    /// <summary>
    /// Represents the result of the game as it is now.
    /// </summary>    
    public interface IGameResult
    {
        /// <summary>
        /// The state of the game
        /// </summary>
        IState State { get; set; }
        
        /// <summary>
        /// Index of winning player (starting on 0) or -1 if the winner is unknown
        /// </summary>
        int Winner { get; set; }        

        /// <summary>
        /// The explanation of who and why is the winner
        /// </summary>
        string Explanation { get; set; }
    }
}

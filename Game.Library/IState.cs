namespace Game.Library
{
    /// <summary>
    /// This class represents a "state" or "turn" of the game. 
    /// </summary>
    public interface IState
    {
        /// <summary>
        /// The index (starting with 0) of the current player, that is, the one that needs to play now
        /// </summary>
        int CurrentPlayer { get; }

        /// <summary>
        /// The representation of the current "state" or "turn" of the game
        /// </summary>
        object State { get; }

        /// <summary>
        /// The description of the current "state" or "turn" of the game
        /// </summary>
        string StateDescription { get; }
    }
}

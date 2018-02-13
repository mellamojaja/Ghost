namespace Game.Library
{
    /// <summary>
    /// A player of games
    /// </summary>
    public interface IPlayer
    {
        /// <summary>
        /// The descriptive name of the player
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The player type (<see cref="PlayerType"/>)
        /// </summary>
        PlayerType Type { get; }

        /// <summary>
        /// Returns the new game state after doing a move, or null if there is not possible/needed
        /// to do a move
        /// </summary>
        IState NextMove(IGame game);
        
        /// <summary>
        /// Returns the analysis of the current state of the game
        /// </summary>        
        IStateAnalysis Analyse(IGame game);
    }

    /// <summary>
    /// (For future use) Enum of different type of players
    /// </summary>
    public enum PlayerType
    {
        human,
        ia,
        perfectIa
    }
}

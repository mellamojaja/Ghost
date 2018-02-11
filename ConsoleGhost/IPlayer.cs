namespace ConsoleGhost
{
    public interface IPlayer<IGame, T>
    {
        string Name { get; }
        PlayerType Type { get; }

        /// <summary>
        /// Returns the new game state after doing a move, or null if there is not possible/needed
        /// to do a move
        /// </summary>
        T NextMove(IGame<T> game);
        
        IStateAnalysis Analyse(IGame<T> game);
    }

    public enum PlayerType
    {
        human,
        ia,
        perfectIa
    }
}

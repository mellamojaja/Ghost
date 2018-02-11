using System.Collections.Generic;

namespace ConsoleGhost
{
    public interface IGame<T>
    {
        /// <summary>
        /// The descriptive name of the game
        /// </summary>
        string Name { get; }
        T State { get; }        
        IGameResult Result { get; }

        IPlayer<IGame<T>, T> CreatePlayer(string name, PlayerType playerType);
        void AddPlayer(IPlayer<IGame<T>, T> player);
        List<IPlayer<IGame<T>, T>> PlayerList { get; }       

        void Reset();
        void Start();
    }
}

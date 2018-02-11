using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGhost
{
    public interface IGame<T>
    {
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

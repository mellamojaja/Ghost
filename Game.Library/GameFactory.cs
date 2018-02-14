using Game.Library.Impl;
using System;

namespace Game.Library
{
    /// <summary>
    /// Use this class to create new games
    /// </summary>
    public sealed class GameFactory
    {
        private static volatile GameFactory _instance;
        private static object syncRoot = new Object();

        public static GameFactory Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (syncRoot)
                    {
                        if (_instance == null)
                        {
                            _instance = new GameFactory();
                        }
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// Creates a new game of the specified type (<see cref="GameType"/>)
        /// </summary>
        /// <param name="gameType"></param>
        /// <returns></returns>
        public IGame CreateGame(GameType gameType, string name) {
            switch (gameType) {
                case GameType.ghost:
                    return new GhostGame(name);

                default:
                    return null;
            }
        }        
    }
}

﻿using System.Collections.Generic;

namespace Game.Library
{
    /// <summary>
    /// Represents a generic game
    /// </summary>    
    public interface IGame
    {
        /// <summary>
        /// The descriptive name of the game
        /// </summary>
        string Name { get; }
        
        /// <summary>
        /// The description of the current "state" or "turn" of the game
        /// </summary>
        IState State { get; set; }

        /// <summary>
        /// Returns the evaluation of the current state of the game
        /// </summary>
        IGameResult GetResult();

        /// <summary>
        /// Returns the analysis of the current state of the game
        /// </summary>
        IStateAnalysis GetAnalysis();

        /// <summary>
        /// Returns a new player for this game
        /// </summary>
        /// <param name="name"></param>
        /// <param name="playerType"><see cref="PlayerType"/></param>
        /// <returns></returns>
        IPlayer CreatePlayer(string name, PlayerType playerType);

        /// <summary>
        /// Adds the player to this game
        /// </summary>        
        void AddPlayer(IPlayer player);

        /// <summary>
        /// Returns the list of player for this game
        /// </summary>
        List<IPlayer> PlayerList { get; }       

        /// <summary>
        /// Reset the game status to its initial setup, just ready to start a new game
        /// </summary>
        void Reset();

        /// <summary>
        /// Starts the game, calling the players in order, getting their moves and updating the game state
        /// </summary>
        void Start();
    }

    /// <summary>
    /// (For future use) Enum of different type of games
    /// </summary>
    public enum GameType
    {
        ghost
    }
}

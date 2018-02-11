﻿namespace ConsoleGhost
{
    public interface IStateAnalysis : IGameResult
    {
        /// <summary>
        /// Index of winning player (starting on 0) or -1 if the winner is unknown
        /// </summary>
        int ExpectedWinner { get; set; }        
    }
}

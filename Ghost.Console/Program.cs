using System;
using Game.Library;
using Game.Library.Impl;

namespace ConsoleGhost
{
    class Program
    {
        public static IGame game;
        public static IPlayer player1;        

        static void Main(string[] args)
        {
            game = GameFactory.Instance.CreateGame(GameType.ghost);
            player1 = game.CreatePlayer("Terminator", PlayerType.perfectIa);            
            var line = "";

            Console.WriteLine(string.Format("Welcome to the '{0}' game.", game.Name));
            RestartGame();
            
            while (true)
            {
                ShowAnalysis();                
                if (game.GetResult().Winner > -1)
                {                    
                    if (AskForExit())
                    {
                        return;
                    }
                    else
                    {
                        RestartGame();
                        continue;
                    }
                }

                Console.Write("$" + game.State.StateDescription + ": ");
                if (game.State.CurrentPlayer == 0)
                {
                    // Human plays
                    line = Console.ReadLine();
                    if (line == "exit")
                    {
                        return;
                    }
                    game.State = new GhostGameState(game.State.StateDescription + (line.TrimStart())[0]);                    
                }
                else
                {
                    // Computer plays              
                    var newState = player1.NextMove(game) as GhostGameState;
                    Console.Write(newState.Word[newState.Word.Length-1]);
                    Console.WriteLine("");
                    game.State = newState;
                }                
            }            
        }

        public static void RestartGame()
        {
            game.Reset();                       
            Console.WriteLine("New Game. You play first");
        }

        public static bool AskForExit()
        {
            Console.WriteLine("Press Enter to play again, or type 'exit' to finish");
            var line = Console.ReadLine();
            if (line == "exit")
            {
                return true;
            }
            return false;
        }

        public static void ShowAnalysis()
        {
            var analysis = game.GetAnalysis();

            if (analysis.Winner > -1)
            {
                Console.WriteLine(string.Format("Player {0} wins, because {1}", analysis.Explanation));
            }
            else 
            {
                Console.WriteLine(string.Format("The game continues but {0}", analysis.Help));
            }
        }

    }
}

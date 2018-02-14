using System;
using Game.Library;

namespace ConsoleGhost
{
    class Program
    {
        public static IGame game;
        public static IPlayer computerPlayer;
        public static IPlayer humanPlayer;

        static void Main(string[] args)
        {
            game = GameFactory.Instance.CreateGame(GameType.ghost, "Optimal Ghost");
            humanPlayer = game.CreatePlayer("Donald Trump", PlayerType.human);
            computerPlayer = game.CreatePlayer("C3PO", PlayerType.perfectIa);
            game.AddPlayer(humanPlayer);
            game.AddPlayer(computerPlayer);

            Console.WriteLine(string.Format("Welcome to the '{0}' game.", game.Name));
            RestartGame();
            
            while (true)
            {
                ShowAnalysis();
                CheckIfTereIsAWinnerAndAskToLeave();

                Console.Write("$" + game.State.StateDescription + ": ");
                game.PlayNextTurn();
                if (game.State.CurrentPlayer == 0)
                {
                    // Has just moved the computer. Give som feedback in the screen
                    Console.Write(game.State.StateDescription[game.State.StateDescription.Length-1]);
                    Console.WriteLine("");
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

        public static void CheckIfTereIsAWinnerAndAskToLeave()
        {
            if (game.Result.Winner > -1)
            {
                if (AskForExit())
                {
                    Environment.Exit(0);
                }
                else
                {
                    RestartGame();                    
                }
            }
        }

        public static void ShowAnalysis()
        {
            var analysis = game.Analysis;

            if (analysis.Winner > -1)
            {
                Console.WriteLine(string.Format("Player {0} wins, because {1}", analysis.Winner, analysis.Explanation));
            }
            else 
            {
                Console.WriteLine(string.Format("The game continues but {0}", analysis.Help));
            }
        }

    }
}

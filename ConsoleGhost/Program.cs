using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleGhost.Impl;

namespace ConsoleGhost
{
    class Program
    {
        public static IGame<GhostGameState> game;
        public static IPlayer<IGame<GhostGameState>, GhostGameState> player1;
        public static IPlayer<IGame<GhostGameState>, GhostGameState> player2;        
        public static IGameResult gameResult;

        static void Main(string[] args)
        {
            game = new GhostGame();
            player1 = game.CreatePlayer("Terminator", PlayerType.perfectIa);            
            var line = "";

            Console.WriteLine(string.Format("Welcome to the '{0}' game.", game.Name));
            RestartGame();
            
            while (true)
            {
                gameResult = player1.Analyse(game);
                Console.WriteLine(gameResult.Explanation);
                if (gameResult.Winner > -1)
                {
                    Console.WriteLine(string.Format("Player {0} wins", gameResult.Winner));
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

                Console.Write("$" + game.State.Word + ": ");
                if (game.State.CurrentPlayer == 0)
                {
                    // Human plays
                    line = Console.ReadLine();
                    if (line == "exit")
                    {
                        break;
                    }                 
                    game.State.Word = game.State.Word + (line.TrimStart())[0];
                    game.State.CurrentPlayer = 1;
                }
                else
                {
                    // Computer plays              
                    var newState = player1.NextMove(game);
                    Console.Write(newState.Word[newState.Word.Length-1]);
                    Console.WriteLine("");
                    game.State.Word = newState.Word;
                    game.State.CurrentPlayer = 0;
                }                
            }            
        }

        public static void RestartGame()
        {
            game.Reset();
            gameResult = player1.Analyse(game);            
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

    }
}

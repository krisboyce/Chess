using Model;
using Model.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chess
{
    class Program
    {
        private static bool GameRunning = true;
        private static Player PlayerOne { get; set; }
        private static Player PlayerTwo { get; set; }
        private static Side PlayerTurn = Side.White;
        static void Main(string[] args)
        {
            SetupPlayers();
            while (GameRunning)
            {
                GameLoop();
            }
        }

        private static void SetupPlayers()
        {
            Console.WriteLine("Player One, enter your name: ");
            var playerOneName = Console.ReadLine();
            var playerOneSide = 3;
            while (playerOneSide > 2 && playerOneSide > 0)
            {
                Console.Clear();
                Console.WriteLine("Please choose your side. \n    1. White\n    2. Black");
                try
                {
                    playerOneSide = Convert.ToInt32(Console.ReadLine());
                }
                catch(FormatException e)
                {}
            }

            Player playerOne = new Player()
            {
                Name = playerOneName,
                Side = (Side)playerOneSide-1
            };

            Console.WriteLine($"You have chosen to be the {playerOne.Side.ToString()} side.");
            Thread.Sleep(1250);

            Console.Clear();
            Console.WriteLine("Player Two, enter your name: ");
            var playerTwoName = Console.ReadLine();

            Player playerTwo = new Player()
            {
                Name = playerTwoName,
                Side = playerOneSide == 1 ? Side.Black : Side.White
            };

            Console.WriteLine($"You are the {playerTwo.Side.ToString()} side.");

            Thread.Sleep(1250);

            PlayerOne = playerOne;
            PlayerTwo = playerTwo;

            Console.Clear();
            Console.WriteLine($"Hello {PlayerOne.Name} and {PlayerTwo.Name}.");
            Console.WriteLine("Press any key to start the game.");
            Console.ReadKey();
        }

        private static void GameLoop()
        {
            Graphics.Display();
            var ActivePlayer = PlayerOne.Side == PlayerTurn ? PlayerOne : PlayerTwo;
            Console.WriteLine($"{ActivePlayer.Name}, It is your move.");
            Console.ReadLine();

            PlayerTurn = PlayerTurn == Side.White ? Side.Black : Side.White;
        }
    }
}

using Logic;
using Model;
using Model.Constants;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Logic.Command;

namespace Chess
{
    public class Program
    {
        private static bool GameRunning = false;
        private static bool InMenu = true;
        private static Player PlayerOne { get; set; }
        private static Player PlayerTwo { get; set; }
        private static Side PlayerTurn = Side.White;
        private static string LastTurnResult = "";
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Console Chess. Type help to begin.");
            while (InMenu)
            {
                var command = Console.ReadLine();
                var gameCommand = Turn.GetCommand(null, command);
                if(gameCommand != null)
                {
                    if(gameCommand.Type.Equals(TurnType.Move) || gameCommand.Type.Equals(TurnType.Save))
                    {
                        Console.WriteLine("Invalid Command. Type help for more options.");
                    }else
                    {
                        var commandResult = Turn.ExecuteCommand(gameCommand);

                        if (!commandResult.Success)
                        {
                            Console.WriteLine(commandResult.ErrorMessage);
                            continue;
                        }

                        switch (gameCommand.Type)
                        {
                            case TurnType.Play:
                                InMenu = false;
                                GameRunning = true;
                                Console.WriteLine(commandResult.ResultMessage);
                                SetupPlayers();
                                break;
                            case TurnType.Quit:
                                Console.WriteLine(commandResult.ResultMessage);
                                InMenu = false;
                                break;
                            case TurnType.Load:
                                InMenu = false;
                                LoadGame(Turn.ExecuteCommand(gameCommand).ResultMessage);
                                break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Unrecognized Command. Type help for more options.");
                }

            }
            Console.Clear();
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
            Console.WriteLine(LastTurnResult);

            var ActivePlayer = PlayerOne.Side == PlayerTurn ? PlayerOne : PlayerTwo;

            Console.WriteLine($"{ActivePlayer.Name}, It is your move.");
            var turnCommand = Console.ReadLine();
            
            TurnCommand command = Turn.GetCommand(ActivePlayer, turnCommand);
            if (command != null)
            {
                var executeResult = Turn.ExecuteCommand(command);
                Console.WriteLine(executeResult.Success ? executeResult.ResultMessage : executeResult.ErrorMessage);

                if (command.Type.Equals(TurnType.Move) && executeResult.Success)
                {
                    PlayerTurn = PlayerTurn.Equals(Side.White) ? Side.Black : Side.White;
                }
            }
            else
            {
                Console.WriteLine(Help.Action().ResultMessage);
            }
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }

        private static void LoadGame(string fileName)
        {

        }
    }
}

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
        private static bool _gameRunning;
        private static bool _inMenu = true;
        private static Player PlayerOne { get; set; }
        private static Player PlayerTwo { get; set; }
        private static Side _playerTurn = Side.White;
        private static Board _board;
        public static void Main()
        {
            Console.WriteLine("Welcome to Console Chess. Type help to begin.");
            while (_inMenu)
            {
                var command = Console.ReadLine();
                var gameCommand = Turn.GetMenuCommand(command);
                if(gameCommand != null)
                {
                    if(gameCommand.Type.Equals(CommandType.Move) || gameCommand.Type.Equals(CommandType.Save))
                    {
                        Console.WriteLine("Invalid Command. Type help for more options.");
                    }else
                    {
                        var commandResult = Turn.ExecuteCommand(gameCommand);

                        if (!commandResult.Success)
                        {
                            Console.WriteLine(commandResult.Message);
                            continue;
                        }

                        switch (gameCommand.Type)
                        {
                            case CommandType.Play:
                                _inMenu = false;
                                _gameRunning = true;
                                Console.WriteLine(commandResult.Message);
                                SetupPlayers();
                                break;
                            case CommandType.Quit:
                                Console.WriteLine(commandResult.Message);
                                _inMenu = false;
                                break;
                            case CommandType.Load:
                                _inMenu = false;
                                LoadGame(Turn.ExecuteCommand(gameCommand).Message);
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
            while (_gameRunning)
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

            _board = new Board()
            {
                Top = PlayerTwo.Side,
                Bottom = PlayerOne.Side
            };

            _board.SetGrid(_board.SetupPeices());
        }

        private static void GameLoop()
        {
            Graphics.Display(_board);

            var ActivePlayer = PlayerOne.Side == _playerTurn ? PlayerOne : PlayerTwo;

            Console.WriteLine($"{ActivePlayer.Name}, It is your move.");
            var turnCommand = Console.ReadLine();
            
            GameCommand command = Turn.GetGameCommand(_board, ActivePlayer, turnCommand);
            if (command != null)
            {
                var executeResult = Turn.ExecuteCommand(command);
                Console.WriteLine(executeResult.Success ? executeResult.Message : executeResult.Message);

                if (command.Type.Equals(CommandType.Move) && executeResult.Success)
                {
                    _playerTurn = _playerTurn.Equals(Side.White) ? Side.Black : Side.White;
                }
            }
            else
            {
                Console.WriteLine(Help.Action().Message);
            }
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }

        private static void LoadGame(string fileName)
        {

        }
    }
}

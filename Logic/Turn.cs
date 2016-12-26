using Model;
using Model.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Command;
using Model.interfaces;

namespace Logic
{
    public static class Turn
    {
        public static GameCommand GetGameCommand(Board board, Player player, string commandString)
        {
            var command = new GameCommand()
            {
                Player = player,
                Board = board,
                Type = GetCommandType(commandString),
                Arguments = GetCommandArguments(commandString)
            };
            return command;
        }

        public static MenuCommand GetMenuCommand(string commandString)
        {
            var command = new MenuCommand()
            {
                Arguments = GetCommandArguments(commandString)
            };
            return command;
        }

        private static List<string> GetCommandArguments(string commandString)
        {
            var commandComponents = commandString.Split(' ').ToList();
            return commandComponents.Skip(1).ToList();
        }

        private static CommandType GetCommandType(string commandString)
        {
            var commandComponents = commandString.Split(' ').ToList();
            switch (commandComponents.First().ToLower())
            {
                case "help":
                case "h":
                    return CommandType.Help;
                case "move":
                case "m":
                    return CommandType.Move;
                case "save":
                case "s":
                    return CommandType.Save;
                case "q":
                    return CommandType.Quit;
                case "load":
                case "l":
                    return CommandType.Load;
                case "play":
                case "p":
                    return CommandType.Play;
                default:
                    return CommandType.Help;
            }
        }
        private static bool ValidateArguments(ICommand command)
        {
            var args = command.Arguments.ToList();
            switch (command.Type)
            {
                case CommandType.Help:
                    return args.Count == 1 || args.Count == 0;
                case CommandType.Move:
                    return args.Count > 1;
                case CommandType.Play:
                    return args.Count == 0;
                case CommandType.Quit:
                    return args.Count == 0;
                case CommandType.Save:
                    return args.Count == 1;
                case CommandType.Load:
                    return args.Count == 1;
                default:
                    return false;
            }
        }

        public static CommandResult ExecuteCommand(ICommand command)
        {
            if (!ValidateArguments(command))
                return new CommandResult
                {
                    Success = false,
                    Message =
                        "Invalid arguments for command: " + command.Type + "\n" +
                        Help.Action(command.Type.ToString())
                };

            switch (command.Type)
            {
                case CommandType.Help:
                    return Help.Action();
                case CommandType.Play:
                    return new CommandResult() {Success = true, Message = "Beginning Game..."};
                case CommandType.Quit:
                    return new CommandResult() {Success = true, Message = "Quitting Game..."};
                case CommandType.Move:
                    return Move.Action(command as GameCommand);
                default:
                    return Help.Action();
            }
        }
    }
}

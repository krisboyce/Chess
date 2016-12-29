using Model;
using Model.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Command;

namespace Logic
{
    public static class Turn
    {
        public static TurnCommand GetCommand(Player player, string commandString)
        {
            var command = ParseCommandString(commandString);
            if (command == null)
                return null;

            command.Player = player;
            return command;
        }

        private static TurnCommand ParseCommandString(string commandString)
        {
            var command = new TurnCommand();
            var commandComponents = commandString.Split(' ').ToList();
            switch (commandComponents.First().ToLower())
            {
                case "help":
                case "h":
                    command.Type = TurnType.Help;
                    break;
                case "move":
                case "m":
                    command.Type = TurnType.Move;
                    break;
                case "quit":
                case "q":
                    command.Type = TurnType.Quit;
                    break;
                case "save":
                case "s":
                    command.Type = TurnType.Save;
                    break;
                case "load":
                case "l":
                    command.Type = TurnType.Load;
                    break;
                case "play":
                case "p":
                    command.Type = TurnType.Play;
                    break;
                default:
                    return null;
            }
            command.PopulateArguments(commandComponents.Skip(1).ToList());
            return command;
        }

        private static void PopulateArguments(this TurnCommand command, List<string> args)
        {
            command.Arguments.AddRange(args);
        }
        private static bool ValidateArguments(this TurnCommand command)
        {
            switch (command.Type)
            {
                case TurnType.Help:
                    return command.Arguments.Count == 1 || command.Arguments.Count == 0;
                case TurnType.Move:
                    return command.Arguments.Count > 1;
                case TurnType.Play:
                    return command.Arguments.Count == 0;
                case TurnType.Quit:
                    return command.Arguments.Count == 0;
                case TurnType.Save:
                    return command.Arguments.Count == 1;
                case TurnType.Load:
                    return command.Arguments.Count == 1;
                default:
                    return false;
            }
        }

        public static CommandResult ExecuteCommand(TurnCommand command)
        {
            if (!command.ValidateArguments())
                return new CommandResult
                {
                    Success = false,
                    ErrorMessage =
                        "Invalid arguments for command: " + command.Type + "\n" +
                        Help.Action(command.Type.ToString())
                };

            switch (command.Type)
            {
                case TurnType.Help:
                    return Help.Action();
                case TurnType.Play:
                    return new CommandResult() {Success = true, ResultMessage = "Beginning Game..."};
                case TurnType.Quit:
                    return new CommandResult() {Success = true, ResultMessage = "Quitting Game..."};
                case TurnType.Move:
                    return Move.Action(command);
                default:
                    return Help.Action();
            }
        }
    }
}

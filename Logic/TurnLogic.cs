using Model;
using Model.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public static class TurnLogic
    {
        public static TurnCommand GetCommand(Player player, string commandString)
        {
            var command = ParseCommandString(commandString);
            if (command == null)
                return null;

            command.player = player;
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
            return command;
        }
        
        private static bool ValidateArguments(this TurnCommand command)
        {
            switch (command.Type)
            {
                case TurnType.Help:
                    return command.Arguments.Count == 1 || command.Arguments.Count == 0;
                case TurnType.Move:
                    return false;
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

        public static string ExecuteCommand(TurnCommand command)
        {
            if(!command.ValidateArguments())
                return "Invalid arguments for command: " + command.Type.ToString() + "\n" + HelpAction(command.Type.ToString());

            switch (command.Type)
            {
                case TurnType.Help:
                    return HelpAction();
                case TurnType.Play:
                    return "Beginning Game...";
                case TurnType.Quit:
                    return "Quitting Game...";
                default:
                    return HelpAction();
            }
        }

        public static string HelpAction(string command = "")
        {
            switch (command.ToLower())
            {
                case "help":
                case "h":
                    return HelpStrings.HELP;
                case "move":
                case "m":
                    return HelpStrings.MOVE;
                case "quit":
                case "q":
                    return HelpStrings.QUIT;
                case "play":
                    return HelpStrings.PLAY;
                case "save":
                case "s":
                    return HelpStrings.SAVE;
                case "load":
                case "l":
                    return HelpStrings.LOAD;
                default:
                    return HelpStrings.HELP;
            }
        }
    }
}

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
            command.player = player;
            var valid = command.ValidateArguments();
            return valid ? command : null;
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
                default:
                    command.Type = TurnType.Help;
                    break;
            }
            return command;
        }
        
        private static bool ValidateArguments(this TurnCommand command)
        {

            return false;
        }

        public static bool ExecuteCommand(TurnCommand command)
        {
            switch (command.Type)
            {

            }
            return false;
        }

        public static void HelpAction(string command)
        {

        }
    }
}

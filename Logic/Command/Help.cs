using Model.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Logic.Command
{
    public static class Help
    {
        public static CommandResult Action(string command = "")
        {
            var commandResult = new CommandResult();
            switch (command.ToLower())
            {
                case "help":
                case "h":
                    commandResult.Message = HelpStrings.HELP;
                    break;
                case "move":
                case "m":
                    commandResult.Message = HelpStrings.MOVE;
                    break;
                case "quit":
                case "q":
                    commandResult.Message = HelpStrings.QUIT;
                    break;
                case "play":
                    commandResult.Message = HelpStrings.PLAY;
                    break;
                case "save":
                case "s":
                    commandResult.Message = HelpStrings.SAVE;
                    break;
                case "load":
                case "l":
                    commandResult.Message = HelpStrings.LOAD;
                    break;
                default:
                    commandResult.Message = HelpStrings.HELP;
                    break;
            }
            return commandResult;
        }
    }
}

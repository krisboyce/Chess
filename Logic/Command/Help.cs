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
                    commandResult.ResultMessage = HelpStrings.HELP;
                    break;
                case "move":
                case "m":
                    commandResult.ResultMessage = HelpStrings.MOVE;
                    break;
                case "quit":
                case "q":
                    commandResult.ResultMessage = HelpStrings.QUIT;
                    break;
                case "play":
                    commandResult.ResultMessage = HelpStrings.PLAY;
                    break;
                case "save":
                case "s":
                    commandResult.ResultMessage = HelpStrings.SAVE;
                    break;
                case "load":
                case "l":
                    commandResult.ResultMessage = HelpStrings.LOAD;
                    break;
                default:
                    commandResult.ResultMessage = HelpStrings.HELP;
                    break;
            }
            return commandResult;
        }
    }
}

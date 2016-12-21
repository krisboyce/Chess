using Model.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Command
{
    public static class Help
    {
        public static string Action(string command = "")
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

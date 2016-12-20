using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Constants
{
    public class HelpStrings
    {
        public const string HELP = @"List of available commands: 
            Help - displays this text. type help <command> for usage.
            Play - starts a new game of chess.
            Move - moves a peice.
            Quit - quits current game or exits application
            Save - saves current game to file
            Load - loads a game from file";

        public const string MOVE = @"Moves a peice from one tile to another. 
            Usage: move a1 b3";

        public const string QUIT = @"Concedes victory to opponent. Closes app from start menu.
            Usage: quit
                Confirmation is required.";

        public const string SAVE = @"Saves the current game
            Usage: save <filename>";

        public const string LOAD = @"Loads a saved game of chess. Can only be used from start menu.
            Usage: load <filename>";

        public const string PLAY = @"Begins a new game of chess. 
            Usage: play";
    }
}

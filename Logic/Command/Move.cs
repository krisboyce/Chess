using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.Constants;

namespace Logic.Command
{
    public static class Move
    {
        public static List<int> ParseMove(string initial, string destination)
        {
            if (initial.Length != 2 || destination.Length != 2)
                return null;

            initial = initial.ToLower();
            destination = destination.ToLower();

            var coords = new List<int>();
            var letterCoords = Letters.Coords.ToLower();
            if (!letterCoords.Contains(initial[0]) || !letterCoords.Contains(destination[0]))
                return null;
            try
            {
                coords.Add(letterCoords.IndexOf(initial[0]));
                coords.Add(8 - Convert.ToInt32(initial[1].ToString()));

                coords.Add(letterCoords.IndexOf(destination[0]));
                coords.Add(8 - Convert.ToInt32(destination[1].ToString()));
            }
            catch (FormatException e)
            {
                return null;
            }
            

            return coords;
        }
        public static CommandResult Action(TurnCommand command)
        {
            var commandResult = new CommandResult();
            var coords = ParseMove(command.Arguments[0], command.Arguments[1]);

            if (coords == null)
                commandResult.Success = false;
                commandResult.ErrorMessage = "Invalid move. Malformed Coordinates.";

            var board = Board.GetInstance();
            var peice = board.GetPeice(coords[0], coords[1]);


            if (!peice.Side.Equals(command.Player.Side))
            {
                commandResult.Success = false;
                commandResult.ErrorMessage = "Invalid move. You can only move your peices.";
                return commandResult;
            }

            if (!PeiceMovement.CanMove(peice, coords[2] - coords[0], coords[3] - coords[1]))
            {
                commandResult.Success = false;
                commandResult.ErrorMessage = "Invalid move. That move is illegal.";
                return commandResult;
            }

            peice = board.MovePeice(coords[0], coords[1], coords[2], coords[3]);
            commandResult.Success = true;
            commandResult.ResultMessage = $"Moved {peice.Type} to {command.Arguments[1]}";
            return commandResult;
        }
    }
}

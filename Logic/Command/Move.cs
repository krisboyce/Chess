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
        public static string Action(TurnCommand command)
        {
            var coords = ParseMove(command.Arguments[0], command.Arguments[1]);

            if (coords == null)
                return "Invalid move. Malformed Coordinates.";

            var board = Board.GetInstance();
            var peice = board.GetPeice(coords[0], coords[1]);


            if (!peice.Side.Equals(command.Player.Side))
                return "Invalid move. You can only move your peices.";

            if (!PeiceMovement.CanMove(peice, coords[2] - coords[0], coords[3] - coords[1]))
                return "Invalid move. That move is illegal.";

            peice = board.MovePeice(coords[0], coords[1], coords[2], coords[3]);

            return $"Moved {peice.Type} to {command.Arguments[1]}";
        }
    }
}

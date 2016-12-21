using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.Constants;

namespace Logic.Command
{
    public static class MoveLogic
    {
        public static List<int> ParseMove(string initial, string destination)
        {
            if (initial.Length != 2 || destination.Length != 2)
                return null;

            var coords = new List<int>();

            if (!Letters.Coords.Contains(initial[0]) || Letters.Coords.Contains(destination[0]))
                return null;

            coords.Add(Letters.Coords.IndexOf(initial[0]));
            coords.Add(initial[1]);

            coords.Add(Letters.Coords.IndexOf(destination[0]));
            coords.Add(destination[1]);

            return coords;
        }
        public static string Action(int x1, int y1, int x2, int y2)
        {
            var board = Board.GetInstance();
            var peice = board.GetPeice(x1, x2);
            if (!PeiceMovement.CanMove(peice, x1 - x2, y1 - y2))
                return "";

            board.MovePeice(x1, y1, x2, y2);

            return "Invalid move.";
        }
    }
}

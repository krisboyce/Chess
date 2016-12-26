using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.Constants;
using Model.interfaces;

namespace Logic.PeiceLogic
{
    public static class Castle
    {
        public static bool CanMove(Board board, Peice peice, int dX, int dY)
        {
            if ((dX != 0 && dY != 0) || (dX + dY) == 0)
                return false;
            if (peice.X + dX < 0 | peice.X + dX > 7)
                return false;
            if (peice.Y + dY < 0 | peice.Y + dY > 7)
                return false;

            List<Peice> blockingPeices = new List<Peice>();
            if (dX != 0)
            {
                var dir = dX < 0 ? -1 : 1;
                var cursor = peice.X;
                while (cursor != peice.X + dX)
                {
                    cursor += dir;

                    var bPeice = board.GetPeice(cursor, peice.Y) as Peice;
                    if(bPeice != null)
                        blockingPeices.Add(bPeice);
                }
            }else
            {
                var dir = dY < 0 ? -1 : 1;
                var cursor = peice.Y;
                while (cursor != peice.Y + dY)
                {
                    cursor += dir;

                    var bPeice = board.GetPeice(peice.X, cursor) as Peice;
                    if (bPeice != null)
                        blockingPeices.Add(bPeice);
                }
            }

            if (!blockingPeices.Any())
                return true;

            var king = blockingPeices.FirstOrDefault(x => x.Type.Equals(PeiceType.King) && x.Side.Equals(peice.Side));
            var isCastle = king != null && !king.IsChecked && !king.HasMoved && !peice.HasMoved && dY == 0;

            if (isCastle)
            {
                if (dX == 4)
                {
                    return King.CanMove(board, king, -3, 0);
                }

                if(dX == -2)
                {
                    return King.CanMove(board, king, 2, 0);
                }
            }

            return false;
        }

        public static CommandResult Move(Peice peice, int dX, int dY)
        {
            return CommandResult.GetSuccess("");
        }
    }
}

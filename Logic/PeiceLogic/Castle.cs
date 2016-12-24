using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.Constants;

namespace Logic.PeiceLogic
{
    public static class Castle
    {
        public static bool CanMove(Peice peice, int dX, int dY)
        {
            if ((dX != 0 && dY != 0) || (dX + dY) == 0)
                return false;
            if (peice.X + dX < 0 | peice.X + dX > 7)
                return false;
            if (peice.Y + dY < 0 | peice.Y + dY > 7)
                return false;

            var board = Board.GetInstance();
            List<Peice> blockingPeices = new List<Peice>();
            if (dX != 0)
            {
                var dir = dX < 0 ? -1 : 1;
                var cursor = peice.X;
                while (cursor != peice.X + dX)
                {
                    var bPeice = board.GetPeice(cursor, peice.Y);
                    if(bPeice != null)
                        blockingPeices.Add(bPeice);

                    cursor += dir;
                }
            }else
            {
                var dir = dY < 0 ? -1 : 1;
                var cursor = peice.Y;
                while (cursor != peice.Y + dY)
                {
                    var bPeice = board.GetPeice(peice.X, cursor);
                    if (bPeice != null)
                        blockingPeices.Add(bPeice);

                    cursor += dir;
                }
            }

            if (!blockingPeices.Any())
                return true;

            var king = blockingPeices.First(x => x.Type.Equals(PeiceType.King) && x.Side.Equals(peice.Side));
            var isCastle = !king.IsChecked && !king.HasMoved && !peice.HasMoved && dY == 0;

            if (isCastle)
            {
                if (dX == 4)
                {
                    return King.CanMove(king, -3, 0);
                }

                if(dX == -3)
                {
                    return King.CanMove(king, 2, 0);
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

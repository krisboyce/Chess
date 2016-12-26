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
    public static class King
    {
        public static bool CanMove(IBoard board, IPeice peice, int dX, int dY)
        {
            if (Math.Abs(dX) > 1 || Math.Abs(dY) > 1)
            {
                if (peice.HasMoved)
                    return false;

                if (dY != 0)
                    return false;

                var dir = dX < 0 ? -1 : 1;
                var cursor = peice.X;
                while (cursor != peice.X + dX)
                {
                    if (new PeiceMovement().IsChecked(board, peice.Side.Equals(Side.White) ? Side.Black : Side.White, cursor, peice.Y))
                        return false;

                    cursor += dir;
                }
            }
            else
            {
                return false;
            }

            var blockingPeice = board.GetPeice(peice.X + dX, peice.Y + dY);

            if (blockingPeice.Side.Equals(peice.Side) && !(blockingPeice.Type.Equals(PeiceType.Castle) && !blockingPeice.HasMoved && !peice.HasMoved))
                return false;

            return !new PeiceMovement().IsChecked(board, peice.Side.Equals(Side.White) ? Side.Black : Side.White, peice.X+dX, peice.Y+dY);
        }

        public static CommandResult Move(Peice peice, int dX, int dY)
        {
            throw new NotImplementedException();
        }
    }
}

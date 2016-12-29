using Model;
using Model.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.PeiceLogic
{
    public static class Pawn
    {
        public static bool CanMove(Peice peice, int dX, int dY) {
            var board = Board.GetInstance();
            var validMove = false;
            if (peice.Type != PeiceType.Pawn)
                return false;

            validMove = (peice.Side == board.Top && dY > 0) || (peice.Side == board.Bottom && dY < 0);

            if (!validMove)
                return false;

            if (dX == 0)
                return true;

            var capturePeice = board.GetPeice(peice.X + dX, peice.Y + dY);
            var enPassantPeice = board.GetPeice(peice.X + dX, peice.Y);

            if (capturePeice != null)
                return true;

            if (enPassantPeice?.Type != PeiceType.Pawn)
                return false;

            if (peice.Type == enPassantPeice.Type)
                return false;

            if (!enPassantPeice.HasMoved)
                return false;

            if (enPassantPeice.Side == board.Top && enPassantPeice.Y == 3)
            {
                return true;
            }
            else if (enPassantPeice.Side == board.Bottom && enPassantPeice.Y == 4)
            {
                return true;
            }

            return false;
        }

        public static Peice Move(Peice peice, int dX, int dY)
        {
            var board = Board.GetInstance();
            if (dX != 0)
            {
                var enPassantPeice = board.GetPeice(peice.X + dX, peice.Y);
                if (enPassantPeice != null)
                {
                    board.CapturePeice(enPassantPeice.X, enPassantPeice.Y);
                }
            }

            return peice;
        }
    }
}

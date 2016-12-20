using Model;
using Model.Constants;

namespace Logic
{
    public static class PeiceMovement
    {
        public static bool CanMove(Peice peice, int dX, int dY)
        {
            switch (peice.Type)
            {
                case PeiceType.Pawn:
                    return PawnCanMove(peice, dX, dY);
            }
            return false;
        }

        private static bool PawnCanMove(Peice peice, int dX, int dY)
        {
            var board = Board.getInstance();
            var validMove = false;
            if (peice.Type != PeiceType.Pawn)
                return false;
            validMove = (peice.Side == board.Top && dY < 0) || (peice.Side == board.Bottom && dY > 0);

            if (!validMove)
                return validMove;

            if(dX != 0)
            {
                Peice capturePeice = board.GetPeice(peice.X + dX, peice.Y + dY);
                if (peice != null)
                {
                    if (peice.Side != capturePeice.Side)
                        return true;
                    else
                        return false;
                }

                Peice enPassantPeice = board.GetPeice(peice.X + dX, peice.Y);
                if (enPassantPeice.Type != PeiceType.Pawn)
                    return false;

                if (peice.Type == enPassantPeice.Type)
                    return false;

                if (!enPassantPeice.HasMoved)
                    return false;

                if(enPassantPeice.Side == board.Top && enPassantPeice.Y == 3) {
                    return true;
                }else if(enPassantPeice.Side == board.Bottom && enPassantPeice.Y == 4)
                {
                    return true;
                }
            }
            return false;
        }
    }
}

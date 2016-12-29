using Logic.PeiceLogic;
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
                    return Pawn.CanMove(peice, dX, dY).Success;
                case PeiceType.Castle:
                    return Castle.CanMove(peice, dX, dY);
                case PeiceType.Knight:
                    return false;
                case PeiceType.Bishop:
                    return false;
                case PeiceType.Queen:
                    return false;
                case PeiceType.King:
                    return false;
                default:
                    return false;
            }
        }

        public static bool IsChecked(Peice peice)
        {
            return false;
        }
    }
}

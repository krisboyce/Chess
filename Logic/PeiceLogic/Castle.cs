using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Logic.PeiceLogic
{
    public static class Castle
    {
        public static bool CanMove(Peice peice, int dX, int dY)
        {
            if ((dX != 0 && dY != 0) || (dX + dY) == 0)
                return false;

            var board = Board.GetInstance();
            if(dX != 0)
            {
                var leftMove = dX < 0;
            }else
            {
                var upMove = dY < 0;
            }

            return false;
        }
    }
}

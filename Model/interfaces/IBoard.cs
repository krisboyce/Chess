using System;
using Model.Constants;

namespace Model.interfaces
{
    public interface IBoard
    {
        Side Top { get; set; }
        Side Bottom { get; set; }

        IPeice GetPeice(int x, int y);
        IPeice GetKing(Side side);
        void CapturePeice(int x, int y);
        void MovePeice(int x1, int y1, int x2, int y2);
        void SetGrid(IPeice[][] grid);
        void RecordMove(IPeice prevPeice, IPeice afterPeice);
        Tuple<IPeice, IPeice> GetLastMove();
    }
}
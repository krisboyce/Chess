using Model.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.interfaces;

namespace Model
{
    public sealed class Board : IBoard
    {
        private static readonly object Lock = new object();
        private IPeice[][] _grid;
        public Side Top { get; set; }
        public Side Bottom { get; set; }
        private List<Tuple<IPeice, IPeice>> MoveHistory = new List<Tuple<IPeice, IPeice>>();

        public void RecordMove(IPeice prevPeice, IPeice afterPeice)
        {
            MoveHistory.Add(new Tuple<IPeice, IPeice>(prevPeice, afterPeice));
        }

        public Tuple<IPeice, IPeice> GetLastMove()
        {
            return MoveHistory.LastOrDefault() ?? new Tuple<IPeice, IPeice>(null, null);
        }

        public void SetGrid(IPeice[][] grid)
        {
            lock (Lock)
            {
                _grid = grid;
            }
        }

        public Peice[][] SetupPeices()
        {
            var tempGrid = new Peice[8][];
            for (var x = 0; x < 8; x++)
            {
                tempGrid[x] = new Peice[8];
                for (var y = 0; y < 8; y++)
                {
                    var type = PeiceType.Pawn;
                    if (y == 0 || y == 7)
                    {
                        switch (x)
                        {
                            case 0:
                            case 7:
                                type = PeiceType.Castle;
                                break;
                            case 1:
                            case 6:
                                type = PeiceType.Knight;
                                break;
                            case 2:
                            case 5:
                                type = PeiceType.Bishop;
                                break;
                            case 3:
                                type = PeiceType.Queen;
                                break;
                            case 4:
                                type = PeiceType.King;
                                break;
                        }
                    }
                    tempGrid[x][y] = new Peice()
                    {
                        Type = type,
                        X = x,
                        Y = y,
                        Side = y > 2 ? Bottom : Top
                    };
                    if (y > 1 && y < 6)
                    {
                        tempGrid[x][y] = null;
                    }
                }
            }
            return tempGrid;
        }

        public IPeice GetPeice(int x, int y)
        {
            IPeice peice;
            lock (Lock)
            {
                peice = _grid[x][y] as Peice;
            }
            return peice;
        }

        public void CapturePeice(int x, int y)
        {
            _grid[x][y] = null;
        }
        public void MovePeice(int x1, int y1, int x2, int y2)
        {
            var peice = GetPeice(x1, y1) as Peice;
            lock (Lock)
            {
                _grid[x2][y2] = peice == null ? null : new Peice(peice)
                {
                    X = x2,
                    Y = y2,
                    HasMoved = true
                };
                _grid[x1][y1] = null;
                RecordMove(peice, _grid[x2][y2]);
            }
        }

        public IPeice GetKing(Side side)
        {
            for(var y = 0; y<_grid.Length; y++)
            {
                for(var x = 0; x<_grid[y].Length; x++)
                {
                    var peice = GetPeice(x, y);
                    if (peice == null)
                        continue;

                    if (peice.Side.Equals(side) && peice.Type.Equals(PeiceType.King))
                        return peice;
                }
            }
            throw new Exception("King not found");
        }
    }
}

using Model.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public sealed class Board
    {
        private static Board _instance = new Board();
        private static readonly object Lock = new object();
        private Peice[][] _grid;
        public Side Top = Side.Black;
        public Side Bottom = Side.White;
        private List<Tuple<Peice, int, int>> MoveHistory = new List<Tuple<Peice, int, int>>();

        private Board()
        {
            if (_instance != null)
            {
                throw new AccessViolationException();
            }
        }

        public void RecordMove(Peice peice, int x, int y)
        {
            MoveHistory.Add(new Tuple<Peice, int, int>(peice, x, y));
        }

        public Tuple<Peice, int, int> GetLastMove()
        {
            return MoveHistory.Last();
        }

        public void SetGrid(Peice[][] grid)
        {
            lock (Lock)
            {
                _grid = grid;
            }
        }

        public Peice[][] SetupPeices()
        {
            var _tempGrid = new Peice[8][];
            for (var x = 0; x < 8; x++)
            {
                _tempGrid[x] = new Peice[8];
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
                    _tempGrid[x][y] = new Peice()
                    {
                        Type = type,
                        X = x,
                        Y = y,
                        Side = y > 2 ? Bottom : Top
                    };
                    if (y > 1 && y < 6)
                    {
                        _tempGrid[x][y] = null;
                    }
                }
            }
            return _tempGrid;
        }
        public static Board GetInstance()
        {
            return _instance;
        }

        public Peice GetPeice(int x, int y)
        {
            Peice peice;
            lock (Lock)
            {
                peice = _grid[x][y];
                if (peice != null)
                {
                    peice.X = x;
                    peice.Y = y;
                }
            }
            return peice;
        }

        public void CapturePeice(int x, int y)
        {
            _grid[x][y] = null;
        }
        public void MovePeice(int x1, int y1, int x2, int y2)
        {
            var peice = GetPeice(x1, y1);
            lock (Lock)
            {
                _grid[x2][y2] = peice;
                _grid[x2][y2].HasMoved = true;
                _grid[x1][y1] = null;
                RecordMove(peice, x2, y2);
            }
        }

        public Peice GetKing(Side side)
        {
            for(var y = 0; y<_grid.Length; y++)
            {
                for(var x = 0; x<_grid[y].Length; x++)
                {
                    var peice = GetPeice(x, y);
                    if (peice.Side.Equals(side) && peice.Type.Equals(PeiceType.King))
                        return peice;
                }
            }
            throw new Exception("King not found");
        }
    }
}

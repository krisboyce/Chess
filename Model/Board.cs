using Model.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Board
    {

        private static Board _instance;
        private static readonly object Lock = new object();
        private Peice[][] _grid;
        public Side Top = Side.Black;
        public Side Bottom = Side.White;
        private List<Tuple<Peice, int, int>> MoveHistory = new List<Tuple<Peice, int, int>>();
        private Board()
        {
            InitializeGrid();
            SetupPeices();
        }

        public void RecordMove(Peice peice, int x, int y)
        {
            MoveHistory.Add(new Tuple<Peice, int, int>(peice, x, y));
        }

        public Tuple<Peice, int, int> GetLastMove()
        {
            return MoveHistory.Last();
        }
        private void InitializeGrid()
        {
            _grid = new Peice[8][];
            for(var i = 0; i<_grid.Length; i++)
            {
                _grid[i] = new Peice[8];
            }
        }


        private void SetupPeices()
        {
            lock (Lock)
            {
                for (var x = 0; x < 8; x++)
                {
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
                        _grid[x][y] = new Peice()
                        {
                            Type = type,
                            X = x,
                            Y = y,
                            Side = y > 2 ? Bottom : Top
                        };
                        if (y > 1 && y < 6)
                        {
                            _grid[x][y] = null;
                        }
                    }
                }
            }
        }
        public static Board GetInstance()
        {
            lock (Lock)
            {
                if (_instance == null)
                    _instance = new Board();
            }
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
        public Peice MovePeice(int x1, int y1, int x2, int y2)
        {

            var peice = GetPeice(x1, y1);
            lock (Lock)
            {
                _grid[x2][y2] = peice;
                _grid[x2][y2].HasMoved = true;
                _grid[x1][y1] = null;
            }
            return _grid[x2][y2];
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

        public static bool IsChecked(Side opponentSide, int x, int y)
        {
            var board = Board.GetInstance();

            //Check knight
            var knightMoves = new Tuple<int, int>[8];
            knightMoves[0] = new Tuple<int, int>(x + 1, y + 2);
            knightMoves[1] = new Tuple<int, int>(x + 1, y + -2);
            knightMoves[2] = new Tuple<int, int>(x + -1, y + 2);
            knightMoves[3] = new Tuple<int, int>(x + -1, y + -2);
            knightMoves[4] = new Tuple<int, int>(x + 2, y + 1);
            knightMoves[5] = new Tuple<int, int>(x + 2, y + -1);
            knightMoves[6] = new Tuple<int, int>(x + -2, y + 1);
            knightMoves[7] = new Tuple<int, int>(x + -2, y + -1);

            if (knightMoves.Select(move => board.GetPeice(move.Item1, move.Item2)).Any(
                possibleKnight => !possibleKnight.Side.Equals(opponentSide)
                                  && possibleKnight.Type.Equals(PeiceType.Knight)))
                return true;

            //check diagonals


            return false;
        }
    }
}

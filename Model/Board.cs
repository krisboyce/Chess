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

        private static volatile Board _instance;
        private static object _lock = new object();
        private static Peice[][] _grid;
        public Side Top = Side.Black;
        public Side Bottom =Side.White;
        private Board()
        {
            InitializeGrid();
            SetupPeices();
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
            for(var x = 0; x<8; x++)
            {
                for(var y = 0; y<8; y++)
                {
                    PeiceType type = PeiceType.Pawn;
                    if(y == 0 || y == 7)
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
                    if (y == 1)
                        y = 5;
                }
            }
        }
        public static Board GetInstance()
        {
            lock (_lock)
            {
                if (_instance == null)
                    _instance = new Board();
            }
            return _instance;
        }

        public Peice GetPeice(int x, int y)
        {
            return _grid[x][y];
        }

        public void MovePeice(int x1, int y1, int x2, int y2)
        {
            var peice = GetPeice(x1, y1);
            _grid[x2][y2] = peice;
            _grid[x2][y2].HasMoved = true;
            _grid[x1][y1] = null;
        }
    }
}

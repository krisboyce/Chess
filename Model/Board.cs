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

        public Side Top;
        public Side Bottom;
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

        }
        public static Board getInstance()
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
            _grid[x1][y1] = null;
        }
    }
}

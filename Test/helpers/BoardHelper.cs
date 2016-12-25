using Model;

namespace Test.helpers
{
    public class BoardHelper
    {
        public static Peice[][] GetEmptyBoard()
        {
            return new []
            {
                new Peice[8],
                new Peice[8],
                new Peice[8],
                new Peice[8],
                new Peice[8],
                new Peice[8],
                new Peice[8],
                new Peice[8]
            };
        }
        public static Peice[] GetEmptyRow()
        {
            return new Peice[8];
        }

        public static Peice[] GetSinglePeiceRow(Peice peice, int y)
        {
            var row = new Peice[8];
            row[y] = peice;
            return row;
        }
    }
}
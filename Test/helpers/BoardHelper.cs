using Model;
using Model.interfaces;

namespace Test.helpers
{
    public class BoardHelper
    {
        public static IPeice[][] GetEmptyBoard()
        {
            return new []
            {
                new IPeice[8],
                new IPeice[8],
                new IPeice[8],
                new IPeice[8],
                new IPeice[8],
                new IPeice[8],
                new IPeice[8],
                new IPeice[8]
            };
        }
        public static IPeice[] GetEmptyRow()
        {
            return new IPeice[8];
        }

        public static IPeice[] GetSinglePeiceRow(Peice peice, int y)
        {
            var row = new IPeice[8];
            row[y] = peice;
            return row;
        }
    }
}
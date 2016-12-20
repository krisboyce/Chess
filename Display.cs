using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public static class Graphics
    {
        private static ConsoleColor WhiteTileColor = ConsoleColor.Gray;
        private static ConsoleColor BlackTileColor = ConsoleColor.DarkRed;
        private static ConsoleColor WhitePeiceColor = ConsoleColor.White;
        private static ConsoleColor BlackPeiceColor = ConsoleColor.Red;
        private static ConsoleColor BorderColor = ConsoleColor.DarkGray;

        private static string _edge =   "|---------------------------------------|";
        private static string _border = "|----|----|----|----|----|----|----|----|";
        public static void Display()
        {
            Console.Clear();
            Console.ForegroundColor = BorderColor;
            Console.WriteLine(_border);



        }
    }
}

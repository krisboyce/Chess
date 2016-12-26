using Model;
using Model.Constants;
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
        public static void Display(Board board)
        {
            Console.Clear();
            Console.ForegroundColor = BorderColor;
            Console.WriteLine(_edge);
            var tileColor = WhiteTileColor;
            for(var y = 0; y<8; y++)
            {
                Console.WriteLine();
                Console.Write("|");
                for(var x = 0; x<8; x++)
                {
                    Console.ForegroundColor = tileColor;
                    Console.Write("*");

                    var peice = board.GetPeice(x, y);
                    if (peice != null)
                    {
                        Console.ForegroundColor = peice.Side.Equals(Side.White) ? WhitePeiceColor : BlackPeiceColor;
                        Console.Write((PeiceDisplay)Enum.ToObject(typeof(PeiceDisplay), (int)peice.Type));
                    }else
                    {
                        Console.Write("**");
                    }
                    Console.ForegroundColor = tileColor;
                    Console.Write("*");
                    Console.ForegroundColor = BorderColor;
                    Console.Write("|");
                    tileColor = tileColor == WhiteTileColor ? BlackTileColor : WhiteTileColor;
                }
                tileColor = tileColor == WhiteTileColor ? BlackTileColor : WhiteTileColor;
                Console.Write($" {8-y}");
                Console.WriteLine();
            }
            Console.WriteLine(_edge);

            for (var x = 0; x < 8; x++)
                Console.Write($"  {Letters.Coords[x]}  ");

            Console.WriteLine();
        }
    }
}

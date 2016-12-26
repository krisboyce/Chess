using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.PeiceLogic;
using Model;
using Model.Constants;
using Test.helpers;
using Xunit;

namespace Test
{
    public class CastleLogicTests
    {
        [Fact]
        public void CastleMovePass()
        {
            //Setup
            var board = new Board
            {
                Top = Side.White,
                Bottom = Side.Black
            };
            var grid = BoardHelper.GetEmptyBoard();
            var row1 = BoardHelper.GetEmptyRow();
            var rook = new Peice
            {
                Type = PeiceType.Castle,
                X = 0,
                Y = 0
            };
            row1[0] = rook;
            grid[0] = row1;
            board.SetGrid(grid);

            Assert.True(Castle.CanMove(board, rook, 7, 0));
            Assert.True(Castle.CanMove(board, rook, 0, 7));
        }

        [Fact]
        public void CastleMoveFail()
        {
            //Setup
            var board = new Board
            {
                Top = Side.White,
                Bottom = Side.Black
            };
            var grid = BoardHelper.GetEmptyBoard();
            var row1 = BoardHelper.GetEmptyRow();
            var rook = new Peice
            {
                Type = PeiceType.Castle,
                X = 0,
                Y = 0
            };
            row1[0] = rook;
            grid[0] = row1;
            board.SetGrid(grid);

            Assert.False(Castle.CanMove(board, rook, 0, 0));
            Assert.False(Castle.CanMove(board, rook, -1, 0));
            Assert.False(Castle.CanMove(board, rook, 8, 0));
            Assert.False(Castle.CanMove(board, rook, 7, 1));
            Assert.False(Castle.CanMove(board, rook, 0, -1));
            Assert.False(Castle.CanMove(board, rook, 0, 8));
        }

        [Fact]
        public void CastleMoveBlockedFail()
        {
            //Setup
            var grid = BoardHelper.GetEmptyBoard();
            var row1 = BoardHelper.GetEmptyRow();
            var row2 = BoardHelper.GetEmptyRow();

            var rook = new Peice
            {
                Type = PeiceType.Castle,
                X = 0,
                Y = 0
            };

            var verticalBlock = new Peice
            {
                Type = PeiceType.Pawn,
                X = 0,
                Y = 1
            };

            var horizontalBlock = new Peice
            {
                Type = PeiceType.Pawn,
                X = 1,
                Y = 0
            };
            row1[0] = rook;
            row1[1] = verticalBlock;
            row2[0] = horizontalBlock;

            grid[0] = row1;
            grid[1] = row2;

            var board = new Board()
            {
                Top = Side.White,
                Bottom = Side.Black
            };
            board.SetGrid(grid);

            Assert.False(Castle.CanMove(board, rook, 0, 7));
            Assert.False(Castle.CanMove(board, rook, 7, 0));

        }

        [Fact]
        public void CastleMoveCastlePass()
        {
            //Setup
            var board = new Board()
            {
                Top = Side.White,
                Bottom = Side.Black
            };
            var grid = BoardHelper.GetEmptyBoard();
            var firstRow = BoardHelper.GetEmptyRow();
            var kingRow = BoardHelper.GetEmptyRow();
            var lastRow = BoardHelper.GetEmptyRow();

            var leftRook = new Peice
            {
                Type = PeiceType.Castle,
                X = 0,
                Y = 0
            };

            var rightRook = new Peice
            {
                Type = PeiceType.Castle,
                X = 7,
                Y = 0
            };

            var king = new Peice
            {
                Type = PeiceType.King,
                X = 5,
                Y = 0
            };

            firstRow[0] = leftRook;
            kingRow[0] = king;
            lastRow[0] = rightRook;

            grid[0] = firstRow;
            grid[5] = kingRow;
            grid[7] = lastRow;

            board.SetGrid(grid);

            Assert.True(Castle.CanMove(board, leftRook, 3, 0));
            Assert.True(Castle.CanMove(board, rightRook, -2, 0));
        }
    }
}

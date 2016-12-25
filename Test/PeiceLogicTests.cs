using Logic;
using Model;
using Model.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.PeiceLogic;
using Test.helpers;
using Xunit;

namespace Test
{

    public class PeiceLogicTests
    {

        [Fact]
        public void PawnPass()
        {
            //Setup
            var board = Board.GetInstance();
            board.Top = Side.White;
            board.Bottom = Side.Black;
            var grid = BoardHelper.GetEmptyBoard();
            var row1 = BoardHelper.GetEmptyRow();
            var pawn = new Peice
            {
                Type = PeiceType.Pawn,
                Side = Side.White,
                X = 0,
                Y = 0
            };
            row1[0] = pawn;
            grid[0] = row1;
            board.SetGrid(grid);

            Assert.True(Pawn.CanMove(pawn, 0, 1).Success);
            Assert.True(Pawn.CanMove(pawn, 0, 2).Success);
        }

        [Fact]
        public void PawnEnPassantPass()
        {
            //Setup
            var board = Board.GetInstance();
            board.Top = Side.White;
            board.Bottom = Side.Black;
            var grid = BoardHelper.GetEmptyBoard();
            var row1 = BoardHelper.GetEmptyRow();
            var row2 = BoardHelper.GetEmptyRow();
            var pawn = new Peice
            {
                Type = PeiceType.Pawn,
                Side = Side.White,
                X = 0,
                Y = 0
            };
            var pawn2 = new Peice
            {
                Type = PeiceType.Pawn,
                Side = Side.Black,
                X = 0,
                Y = 0,
                HasMoved = true
            };
            row1[4] = pawn;
            row2[4] = pawn2;

            grid[0] = row1;
            grid[1] = row2;
            board.SetGrid(grid);

            board.RecordMove(pawn2, 1, 4);

            Assert.True(Pawn.CanMove(pawn, 1, 1).Success);
        }

        [Fact]
        public void PawnDoubleMoveFail()
        {
            //Setup
            var board = Board.GetInstance();
            board.Top = Side.White;
            board.Bottom = Side.Black;
            var grid = BoardHelper.GetEmptyBoard();
            var row1 = BoardHelper.GetEmptyRow();
            var pawn = new Peice
            {
                Type = PeiceType.Pawn,
                Side = Side.White,
                X = 0,
                Y = 0,
                HasMoved = true
            };
            row1[0] = pawn;
            grid[0] = row1;
            board.SetGrid(grid);

            Assert.False(Pawn.CanMove(pawn, 0, 2).Success);
        }

        [Fact]
        public void PawnBackwardsFail()
        {
            //Setup
            var board = Board.GetInstance();
            board.Top = Side.White;
            board.Bottom = Side.Black;
            var grid = BoardHelper.GetEmptyBoard();
            var row1 = BoardHelper.GetEmptyRow();
            var pawn = new Peice
            {
                Type = PeiceType.Pawn,
                Side = Side.White,
                X = 0,
                Y = 0,
                HasMoved = true
            };
            row1[1] = pawn;
            grid[0] = row1;
            board.SetGrid(grid);

            Assert.False(Pawn.CanMove(pawn, 0, -1).Success);
        }

    }
}

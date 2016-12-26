using Logic;
using Model;
using Model.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.PeiceLogic;
using Model.interfaces;
using Test.helpers;
using Xunit;

namespace Test
{

    public class PawnLogicTests
    {

        [Fact]
        public void PawnPass()
        {
            //Setup
            var board = new Board()
            {
                Top = Side.White,
                Bottom = Side.Black
            };
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

            Assert.True(Pawn.CanMove(board, pawn, 0, 1).Success);
            Assert.True(Pawn.CanMove(board, pawn, 0, 2).Success);
        }

        [Fact]
        public void PawnEnPassantPass()
        {
            //Setup
            var board = new Board
            {
                Top = Side.White,
                Bottom = Side.Black
            };
            var grid = BoardHelper.GetEmptyBoard();
            var row1 = BoardHelper.GetEmptyRow();
            var row2 = BoardHelper.GetEmptyRow();
            var pawn = new Peice
            {
                Type = PeiceType.Pawn,
                Side = Side.White,
                X = 0,
                Y = 4
            };

            var enPassantPrev = new Peice
            {
                Type = PeiceType.Pawn,
                Side = Side.Black,
                X = 1,
                Y = 2,
                HasMoved = true
            };

            var enPassant = new Peice
            {
                Type = PeiceType.Pawn,
                Side = Side.Black,
                X = 1,
                Y = 4,
                HasMoved = true
            };
            row1[4] = pawn;
            row2[4] = enPassant;

            grid[0] = row1;
            grid[1] = row2;
            board.SetGrid(grid);

            board.RecordMove(enPassantPrev, enPassant);
            Assert.True(Pawn.CanMove(board, pawn, 1, 1).Success);
        }

        [Fact]
        public void PawnEnPassantImmediateFail()
        {
            //Setup
            var board = new Board
            {
                Top = Side.White,
                Bottom = Side.Black
            };
            IPeice[][] grid = BoardHelper.GetEmptyBoard();
            var row1 = BoardHelper.GetEmptyRow();
            var row2 = BoardHelper.GetEmptyRow();
            var pawn = new Peice
            {
                Type = PeiceType.Pawn,
                Side = Side.White,
                X = 0,
                Y = 4
            };
            var pawn2 = new Peice
            {
                Type = PeiceType.Pawn,
                Side = Side.Black,
                X = 1,
                Y = 4,
                HasMoved = true
            };
            row1[4] = pawn;
            row2[4] = pawn2;

            grid[0] = row1;
            grid[1] = row2;
            board.SetGrid(grid);

            var execute = Pawn.CanMove(board, pawn, 1, 1);

            Assert.False(execute.Success);
        }

        [Fact]
        public void PawnEnPassantSingleMoveFail()
        {
            //Setup
            var board = new Board
            {
                Top = Side.White,
                Bottom = Side.Black
            };
            var grid = BoardHelper.GetEmptyBoard();
            var row1 = BoardHelper.GetEmptyRow();
            var row2 = BoardHelper.GetEmptyRow();
            var pawn = new Peice
            {
                Type = PeiceType.Pawn,
                Side = Side.White,
                X = 0,
                Y = 4
            };

            var enPassantPrev = new Peice
            {
                Type = PeiceType.Pawn,
                Side = Side.Black,
                X = 1,
                Y = 3,
                HasMoved = true
            };

            var enPassant = new Peice
            {
                Type = PeiceType.Pawn,
                Side = Side.Black,
                X = 1,
                Y = 4,
                HasMoved = true
            };
            row1[4] = pawn;
            row2[4] = enPassant;

            grid[0] = row1;
            grid[1] = row2;
            board.SetGrid(grid);

            board.RecordMove(enPassantPrev, enPassant);
            Assert.False(Pawn.CanMove(board, pawn, 1, 1).Success);
        }

        [Fact]
        public void PawnDoubleMoveFail()
        {
            //Setup
            var board = new Board
            {
                Top = Side.White,
                Bottom = Side.Black
            };
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

            Assert.False(Pawn.CanMove(board, pawn, 0, 2).Success);
        }

        [Fact]
        public void PawnBackwardsFail()
        {
            //Setup
            var board = new Board
            {
                Top = Side.White,
                Bottom = Side.Black
            };
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

            Assert.False(Pawn.CanMove(board, pawn, 0, -1).Success);
        }

    }
}

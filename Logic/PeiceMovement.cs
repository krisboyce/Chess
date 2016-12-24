using Logic.PeiceLogic;
using Model;
using Model.Constants;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Logic
{
    public class PeiceMovement
    {
        public CommandResult Move(Peice peice, int dX, int dY)
        {
            var result = new CommandResult();
            switch (peice.Type)
            {
                case PeiceType.Pawn:
                    result = Pawn.Move(peice, dX, dY);
                    break;
                case PeiceType.Castle:
                    result = Castle.Move(peice, dX, dY);
                    break;
                case PeiceType.Knight:
                    break;
                case PeiceType.Bishop:
                    break;
                case PeiceType.Queen:
                    break;
                case PeiceType.King:
                    result = King.Move(peice, dX, dY);
                    break;
            }

            var king = Board.GetInstance().GetKing(peice.Side);
            if(IsChecked(king.Side, king.X, king.Y))
            {
                return null;
            }

            return result;
        }

        public bool IsChecked(Side opponentSide, int x, int y)
        {
            var oSide = opponentSide;
            var xPos = x;
            var yPos = y;

            //Check knight
            var checkKnight = Task.Factory.StartNew<bool>(() => CheckKnight(oSide, xPos, yPos));

            //check diagonals
            var checkDiagonal = Task.Factory.StartNew<bool>(() => CheckDiagonal(oSide, xPos, yPos));

            return checkKnight.Result || checkDiagonal.Result;
        }

        private bool CheckKnight(Side opponentSide, int x, int y)
        {
            var board = Board.GetInstance();

            var knightMoves = new Tuple<int, int>[8];
            knightMoves[0] = new Tuple<int, int>(x + 1, y + 2);
            knightMoves[1] = new Tuple<int, int>(x + 1, y + -2);
            knightMoves[2] = new Tuple<int, int>(x + -1, y + 2);
            knightMoves[3] = new Tuple<int, int>(x + -1, y + -2);
            knightMoves[4] = new Tuple<int, int>(x + 2, y + 1);
            knightMoves[5] = new Tuple<int, int>(x + 2, y + -1);
            knightMoves[6] = new Tuple<int, int>(x + -2, y + 1);
            knightMoves[7] = new Tuple<int, int>(x + -2, y + -1);

            return knightMoves.Select(move => board.GetPeice(move.Item1, move.Item2)).Any(
                possibleKnight => !possibleKnight.Side.Equals(opponentSide)
                                  && possibleKnight.Type.Equals(PeiceType.Knight));
        }

        private bool CheckDiagonal(Side opponentSide, int x, int y)
        {
            var board = Board.GetInstance();

            var isChecked = false;

            for (var dir = 0; dir < 4; dir++)
            {
                int xDir;
                int yDir;
                switch (dir)
                {
                    case 0:
                        xDir = -1;
                        yDir = -1;
                        break;
                    case 1:
                        xDir = 1;
                        yDir = -1;
                        break;
                    case 2:
                        xDir = -1;
                        yDir = 1;
                        break;
                    case 3:
                        xDir = 1;
                        yDir = 1;
                        break;
                    default:
                        throw new InvalidOperationException();
                }

                var cursorX = x;
                var cursorY = y;
                var movedX = 0;
                var movedY = 0;

                while (cursorY >= 0 && cursorX >= 0 && cursorY < 8 && cursorX < 8 && !isChecked)
                {
                    cursorY += yDir;
                    cursorX += xDir;
                    movedY += yDir;
                    movedX += xDir;

                    if (cursorY >= 0 && cursorX >= 0 && cursorY < 8 && cursorX < 8)
                        continue;

                    var cursorPeice = board.GetPeice(cursorX, cursorY);

                    if (cursorPeice == null)
                        continue;

                    if (!cursorPeice.Side.Equals(opponentSide))
                        break;

                    var pawnDirection = opponentSide.Equals(Side.White) ? -1 : 1;
                    var pawnChecked = movedY * pawnDirection == movedY
                                      && movedY != 0 && movedX != 0
                                      && cursorPeice.Type.Equals(PeiceType.Pawn);

                    var onePlace = Math.Abs(movedY) <= 1 && Math.Abs(movedX) <= 1;
                    var kingCheck = onePlace && (cursorPeice.Type.Equals(PeiceType.King));

                    var distantCheck = cursorPeice.Type.Equals(PeiceType.Bishop) || cursorPeice.Type == PeiceType.Queen;

                    isChecked = pawnChecked || kingCheck || distantCheck;
                }

                if (isChecked)
                    break;
            }
            return isChecked;
        }

        private bool CheckOrthagonal(Side opponentSide, int x, int y)
        {
            var board = Board.GetInstance();

            var isChecked = false;

            for (var dir = 0; dir < 4; dir++)
            {
                int xDir;
                int yDir;
                switch (dir)
                {
                    case 0:
                        xDir = 0;
                        yDir = -1;
                        break;
                    case 1:
                        xDir = 0;
                        yDir = 1;
                        break;
                    case 2:
                        xDir = -1;
                        yDir = 0;
                        break;
                    case 3:
                        xDir = 1;
                        yDir = 0;
                        break;
                    default:
                        throw new InvalidOperationException();
                }

                var cursorX = x;
                var cursorY = y;
                var movedX = 0;
                var movedY = 0;

                while (cursorY >= 0 && cursorX >= 0 && cursorY < 8 && cursorX < 8 && !isChecked)
                {
                    cursorY += yDir;
                    cursorX += xDir;
                    movedY += yDir;
                    movedX += xDir;

                    if (cursorY >= 0 && cursorX >= 0 && cursorY < 8 && cursorX < 8)
                        continue;

                    var cursorPeice = board.GetPeice(cursorX, cursorY);

                    if (cursorPeice == null)
                        continue;

                    if (!cursorPeice.Side.Equals(opponentSide))
                        break;

                    var pawnDirection = opponentSide.Equals(Side.White) ? -1 : 1;
                    var pawnChecked = movedY * pawnDirection == movedY
                                      && movedY != 0 && movedX != 0
                                      && cursorPeice.Type.Equals(PeiceType.Pawn);

                    var onePlace = Math.Abs(movedY) <= 1 && Math.Abs(movedX) <= 1;
                    var kingCheck = onePlace && (cursorPeice.Type.Equals(PeiceType.King));

                    var distantCheck = cursorPeice.Type.Equals(PeiceType.Bishop) || cursorPeice.Type == PeiceType.Queen;

                    isChecked = pawnChecked || kingCheck || distantCheck;
                }

                if (isChecked)
                    break;
            }
            return isChecked;
        }
    }
}

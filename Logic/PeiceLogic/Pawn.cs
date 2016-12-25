using Model;
using Model.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.PeiceLogic
{
    public static class Pawn
    {
        public static CommandResult CanMove(Peice peice, int dX, int dY) {

            var result = new CommandResult();
            var board = Board.GetInstance();

            if (peice.Type != PeiceType.Pawn)
            {
                result.ErrorMessage = "Peice is not a pawn";
                return result;
            }


            if(!(peice.Side == board.Top && dY > 0 || peice.Side == board.Bottom && dY < 0))
                    return CommandResult.GetFail("Can not move in that direction.");

            if (dX == 0)
            {
                if (dY%2 == 0)
                {
                    if (peice.HasMoved)
                    {
                        return CommandResult.GetFail("Double move is only valid as first move.");
                    }
                    var blockPeice = board.GetPeice(peice.X, peice.Y + dY) ?? board.GetPeice(peice.X, peice.Y + dY/2);
                    if (blockPeice == null)
                    {
                        result.Success = true;
                        result.ResultMessage = "Peice is not blocked.";
                        return result;
                    }
                }
                else
                {
                    var blockPeice = board.GetPeice(peice.X, peice.Y + dY);
                    if (blockPeice == null)
                    {
                        result.Success = true;
                        result.ResultMessage = "Peice is not blocked.";
                        return result;
                    }
                }
            }

            var capturePeice = board.GetPeice(peice.X + dX, peice.Y + dY);
            var enPassantPeice = board.GetPeice(peice.X + dX, peice.Y);

            if (capturePeice != null)
            {
                if (capturePeice.Side.Equals(peice.Side))
                {
                    result.Success = false;
                    result.ErrorMessage = "Can't capture owned peice.";
                }

                result.Success = true;
                result.ResultMessage = "Can capture peice.";
                return result;
            }

            if (enPassantPeice != null && !enPassantPeice.Type.Equals(PeiceType.Pawn))
            {
                result.Success = false;
                result.ResultMessage = "Can only en passant a pawn.";
                return result;
            }

            if (enPassantPeice != null)
            {
                if (peice.Side.Equals(enPassantPeice.Side))
                {
                    result.Success = false;
                    result.ErrorMessage = "Can not capture own peice.";
                    return result;
                }

                if (!enPassantPeice.HasMoved)
                {
                    var lastMove = board.GetLastMove();
                    if (lastMove.Item1.Type.Equals(PeiceType.Pawn))
                    {
                        if (lastMove.Item2 == enPassantPeice.X && (lastMove.Item3 - enPassantPeice.Y)%2 == 0)
                        {

                            return result;
                        }
                    }
                    result.Success = false;
                    result.ErrorMessage = "Capture peice must have moved";
                    return result;
                }
                if (enPassantPeice.Side == board.Top && enPassantPeice.Y == 3)
                {
                    return result;
                }

                if (enPassantPeice.Side == board.Bottom && enPassantPeice.Y == 4)
                {
                    return result;
                }
            }

            return result;
        }

        public static CommandResult Move(Peice peice, int dX, int dY)
        {
            var board = Board.GetInstance();

            var moveCheckResult = CanMove(peice, dX, dY);

            if (!moveCheckResult.Success)
                return moveCheckResult;

            if (dX != 0)
            {
                var enPassantPeice = board.GetPeice(peice.X + dX, peice.Y);
                if (enPassantPeice != null)
                {
                    board.CapturePeice(enPassantPeice.X, enPassantPeice.Y);
                }
            }

            board.MovePeice(peice.X, peice.Y, peice.X + dX, peice.Y + dY);
            var xCoord = Letters.Coords[peice.X + dX];

            return CommandResult.GetSuccess($"Moved {peice.Type} to {xCoord}{peice.Y + dY}");
        }
    }
}

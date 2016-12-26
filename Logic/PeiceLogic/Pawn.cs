using Model;
using Model.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.interfaces;

namespace Logic.PeiceLogic
{
    public static class Pawn
    {
        public static CommandResult CanMove(IBoard board, IPeice peice, int dX, int dY) {

            var result = new CommandResult();

            if (peice.Type != PeiceType.Pawn)
            {
                result.Message = "Peice is not a pawn";
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
                        result.Message = "Peice is not blocked.";
                        return result;
                    }
                }
                else
                {
                    var blockPeice = board.GetPeice(peice.X, peice.Y + dY);
                    if (blockPeice == null)
                    {
                        result.Success = true;
                        result.Message = "Peice is not blocked.";
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
                    result.Message = "Can't capture owned peice.";
                }

                result.Success = true;
                result.Message = "Can capture peice.";
                return result;
            }

            if (enPassantPeice != null && !enPassantPeice.Type.Equals(PeiceType.Pawn))
            {
                result.Success = false;
                result.Message = "Can only en passant a pawn.";
                return result;
            }

            if (enPassantPeice != null)
            {
                if (peice.Side.Equals(enPassantPeice.Side))
                {
                    result.Success = false;
                    result.Message = "Can not capture own peice.";
                    return result;
                }

                if (!enPassantPeice.HasMoved)
                {
                    result.Success = false;
                    result.Message = "Capture peice must have moved";
                    return result;
                }
                if (enPassantPeice.Side == board.Top && enPassantPeice.Y == 3 || enPassantPeice.Side == board.Bottom && enPassantPeice.Y == 4)
                {
                    var lastMove = board.GetLastMove();
                    if (lastMove.Item1 != null && lastMove.Item1.Type.Equals(PeiceType.Pawn))
                    {
                        if (lastMove.Item2.X == enPassantPeice.X && (lastMove.Item2.Y - lastMove.Item1.Y) % 2 == 0)
                        {
                            return CommandResult.GetSuccess("Can capture en passant");
                        }

                        return CommandResult.GetFail("En passant must be done immediatly.");
                    }
                    else
                    {
                        return CommandResult.GetFail("En passant can not be the first move.");
                    }
                }
            }

            return CommandResult.GetFail("Unrecognized pawn move.");
        }

        public static CommandResult Move(IBoard board, IPeice peice, int dX, int dY)
        {
            var moveCheckResult = CanMove(board, peice, dX, dY);

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

namespace Solarisin.Chess.Shared.Models.Pieces;

/// <summary>
///     Models properties of a pawn piece.
/// </summary>
public class Pawn : Piece
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="Pawn" /> class.
    /// </summary>
    /// <param name="player">The player color of the owner of this piece</param>
    /// <param name="position">The initial position of the piece</param>
    public Pawn(Player player, Position position) : base(player, position)
    {
    }

    /// <summary>
    ///     Overridden property to return the Pawn type
    /// </summary>
    public override PieceType Type => PieceType.Pawn;

    /// <summary>
    ///     Determine if a pawn can move to the given new position. This function assumes the
    ///     board is in the standard Black on top, White on bottom format.
    /// </summary>
    /// <param name="board">The current state of the board</param>
    /// <param name="newPosition">The position to test movement validity to</param>
    /// <returns>True, if this pawn can move to the given position based upon the board state</returns>
    protected override bool CanMovePieceTo(Board board, Position newPosition)
    {
        var validMove = false;

        // Pawn can move one space forward or two spaces forward if it hasn't moved yet
        var difference = Position.GetDifference(Position, newPosition);

        if (Player == Player.Black)
            // Pawns move down
            difference.FileDifference = -difference.FileDifference;

        if (board.GetPieceAt(newPosition) == null)
        {
            if (difference.FileDifference == 1 && difference.RankDifference == 0)
                validMove = true;
            else if (difference.FileDifference == 2 && difference.RankDifference == 0 && !HasMoved)
                validMove = true;
        }
        else
        {
            // Can only attack pieces diagonally
            if ((difference.FileDifference == 1 && difference.RankDifference == 1) ||
                (difference.FileDifference == 1 && difference.RankDifference == -1))
                validMove = true;
        }

        // TODO handle "en passant"

        // TODO handle promotion

        if (validMove)
        {
            var possibleBlockers = Position.GetPositionsBetween(Position, newPosition);
            if (possibleBlockers != null)
                foreach (var blocker in possibleBlockers)
                {
                    var pieceAtBlocker = board.GetPieceAt(blocker);
                    if (pieceAtBlocker != null)
                        return false;
                }

            return true;
        }

        return false;
    }
}
namespace Solarisin.Chess.Shared.Models.Pieces;

/// <summary>
///     Models properties of a bishop piece.
/// </summary>
public class Bishop : Piece
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="Bishop" /> class.
    /// </summary>
    /// <param name="player">The player color of the owner of this piece</param>
    /// <param name="position">The initial position of the piece</param>
    public Bishop(Player player, Position position) : base(player, position)
    {
    }

    /// <summary>
    ///     Overridden property to return the Bishop type
    /// </summary>
    public override PieceType Type => PieceType.Bishop;

    /// <summary>
    ///     Determine if a bishop can move to the given new position.
    /// </summary>
    /// <param name="board">The current state of the board</param>
    /// <param name="newPosition">The position to test movement validity to</param>
    /// <returns>True, if this bishop can move to the given position based upon the board state</returns>
    protected override bool CanMovePieceTo(Board board, Position newPosition)
    {
        // Bishop can move any number of spaces diagonally
        var distance = Position.GetDistance(Position, newPosition);
        if (distance.RankDistance == distance.FileDistance)
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
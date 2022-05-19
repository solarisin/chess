namespace Solarisin.Chess.Shared.Models.Pieces;

/// <summary>
///     Models properties of a queen piece.
/// </summary>
public class Queen : Piece
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="Queen" /> class.
    /// </summary>
    /// <param name="player">The player color of the owner of this piece</param>
    /// <param name="position">The initial position of the piece</param>
    public Queen(Player player, Position position) : base(player, position)
    {
    }

    /// <summary>
    ///     Overridden property to return the Queen type
    /// </summary>
    public override PieceType Type => PieceType.Queen;

    /// <summary>
    ///     Determine if a queen can move to the given new position.
    /// </summary>
    /// <param name="board">The current state of the board</param>
    /// <param name="newPosition">The position to test movement validity to</param>
    /// <returns>True, if this queen can move to the given position based upon the board state</returns>
    protected override bool CanMovePieceTo(Board board, Position newPosition)
    {
        // Queen can move any number of spaces in any direction and diagonally
        var distance = Position.GetDistance(Position, newPosition);
        if (distance.RankDistance == 0 || distance.FileDistance == 0 ||
            distance.RankDistance == distance.FileDistance)
        {
            var possibleBlockers = Position.GetPositionsBetween(Position, newPosition);
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
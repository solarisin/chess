namespace Solarisin.Chess.Shared.Models.Pieces;

/// <summary>
///     Models properties of a king piece.
/// </summary>
public class King : Piece
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="King" /> class.
    /// </summary>
    /// <param name="player">The player color of the owner of this piece</param>
    /// <param name="position">The initial position of the piece</param>
    public King(Player player, Position position) : base(player, position)
    {
    }

    /// <summary>
    ///     Overridden property to return the King type
    /// </summary>
    public override PieceType Type => PieceType.King;

    /// <summary>
    ///     Determine if a king can move to the given new position.
    /// </summary>
    /// <param name="board">The current state of the board</param>
    /// <param name="newPosition">The position to test movement validity to</param>
    /// <returns>True, if this king can move to the given position based upon the board state</returns>
    protected override bool CanMovePieceTo(Board board, Position newPosition)
    {
        // King can move one space in any direction
        var distance = Position.GetDistance(Position, newPosition);
        if (distance.RankDistance <= 1 && distance.FileDistance <= 1)
            return true;
        return false;
    }
}
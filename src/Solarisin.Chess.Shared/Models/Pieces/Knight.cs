namespace Solarisin.Chess.Shared.Models.Pieces;

/// <summary>
///     Models properties of a knight piece.
/// </summary>
public class Knight : Piece
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="Knight" /> class.
    /// </summary>
    /// <param name="player">The player color of the owner of this piece</param>
    /// <param name="position">The initial position of the piece</param>
    public Knight(Player player, Position position) : base(player, position)
    {
    }

    /// <summary>
    ///     Overridden property to return the Knight type
    /// </summary>
    public override PieceType Type => PieceType.Knight;

    /// <summary>
    ///     Determine if a knight can move to the given new position.
    /// </summary>
    /// <param name="board">The current state of the board</param>
    /// <param name="newPosition">The position to test movement validity to</param>
    /// <returns>True, if this knight can move to the given position based upon the board state</returns>
    protected override bool CanMovePieceTo(Board board, Position newPosition)
    {
        // Knight must move one space in one direction and two spaces in another
        var distance = Position.GetDistance(Position, newPosition);
        if ((distance.RankDistance == 2 && distance.FileDistance == 1) ||
            (distance.RankDistance == 1 && distance.FileDistance == 2))
            return true;
        return false;
    }
}
using Solarisin.Chess.Shared.Models.Pieces;

namespace Solarisin.Chess.Shared.Models;

public abstract class Piece
{
    /// <summary>
    ///     Constructor for a new instance of the <see cref="Piece" /> class.
    /// </summary>
    /// <param name="player">The player, or color of this piece.</param>
    /// <param name="position">The initial position of the piece.</param>
    protected Piece(Player player, Position position)
    {
        Player = player;
        Position = position;
    }

    /// <summary>
    ///     The type of the piece.
    /// </summary>
    public abstract PieceType Type { get; }

    /// <summary>
    ///     The player, or color, of the piece.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    ///     The current position of this piece.
    /// </summary>
    public Position Position { get; protected set; }

    /// <summary>
    ///     Stores whether the piece has moved or not in this game. Used for determining whether
    ///     a pawn can move two spaces.
    /// </summary>
    public bool HasMoved { get; protected set; }

    /// <summary>
    ///     Abstract method to determine if this piece can move to the given position. Subclasses will
    ///     implement specific logic rules for each piece type.
    /// </summary>
    /// <param name="board">The current state of the board</param>
    /// <param name="newPosition">The position to test movement validity to</param>
    /// <returns>True, if this piece can move to the given position based upon the board state</returns>
    protected abstract bool CanMovePieceTo(Board board, Position newPosition);

    /// <summary>
    ///     Move this piece to the given position.
    /// </summary>
    /// <param name="position">The position to move this piece to.</param>
    public void MovePieceTo(Position position)
    {
        if (position != Position)
        {
            Position = position;
            HasMoved = true;
        }
    }

    /// <summary>
    ///     Determine if this piece can move to the given position.
    /// </summary>
    /// <param name="board">The current board state</param>
    /// <param name="newPosition">The position to check</param>
    /// <returns>true, if this piece is able to move to the given position</returns>
    public bool CanMoveTo(Board board, Position newPosition)
    {
        // Can't move to a space that is occupied by your own piece
        // TODO except castling

        var pieceAtNewPosition = board.GetPieceAt(newPosition);
        if (pieceAtNewPosition != null && pieceAtNewPosition.Player == Player)
            return false;

        return CanMovePieceTo(board, newPosition);
    }

    /// <summary>
    ///     Static method to create a piece based on the given fen character and position.
    /// </summary>
    /// <param name="fenChar">The single fen character describing this piece.</param>
    /// <param name="position">The position of the piece.</param>
    /// <returns>The created piece.</returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static Piece CreatePiece(char fenChar, Position position)
    {
        var player = Game.PlayerFromChar(fenChar);
        var pieceType = Game.PieceTypeFromChar(fenChar);
        return pieceType switch
        {
            PieceType.Pawn => new Pawn(player, position),
            PieceType.Rook => new Rook(player, position),
            PieceType.Knight => new Knight(player, position),
            PieceType.Bishop => new Bishop(player, position),
            PieceType.Queen => new Queen(player, position),
            PieceType.King => new King(player, position),
            _ => throw new InvalidOperationException("Unknown piece type")
        };
    }
}
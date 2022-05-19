namespace Solarisin.Chess.Shared.Models;

/// <summary>
///     Used to logically represent a chess board.
/// </summary>
public class Board
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="Board" /> class.
    /// </summary>
    public Board()
    {
        Pieces = CreatePieces();
    }

    /// <summary>
    ///     The list of all chess pieces currently on the board
    /// </summary>
    public List<Piece> Pieces { get; }

    /// <summary>
    ///     Create the chess pieces based upon the given FEN string
    /// </summary>
    /// <returns>The list of pieces created.</returns>
    private List<Piece> CreatePieces(string fen = "")
    {
        if (fen == "") // If no FEN string is given, use a fen string describing the standard starting position
            fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";

        if (!Game.IsValidFen(fen))
            throw new ArgumentException("Invalid FEN string");

        var pieces = new List<Piece>();
        var fenRows = fen.Split('/');
        var rank = 1;
        foreach (var fenRow in fenRows)
        {
            var file = File.A;
            foreach (var fenChar in fenRow)
                if (char.IsDigit(fenChar))
                {
                    // Numbers increment the file
                    file += int.Parse(fenChar.ToString());
                }
                else
                {
                    var piece = Piece.CreatePiece(fenChar, new Position(rank, file));
                    pieces.Add(piece);
                    file++;
                }

            rank++;
        }

        return pieces;
    }

    public Piece? GetPieceAt(Position newPosition)
    {
        var piece = Pieces.FirstOrDefault(p => p.Position.Equals(newPosition));
        return piece;
    }

    public void MovePiece(Position newPosition)
    {
        // TODO do anything else that needs to happen when a piece is captured
        var piece = GetPieceAt(newPosition);
        if (piece != null)
            Pieces.Remove(piece);

        // such as recording history or tallying captured pieces
    }
}
namespace Solarisin.Chess.Shared.Models;

public enum PieceType
{
    None,
    Rook,
    Knight,
    Bishop,
    Queen,
    King,
    Pawn
}

public enum Player
{
    White,
    Black
}

public enum File
{
    A,
    B,
    C,
    D,
    E,
    F,
    G,
    H
}

public class Game
{
   /// <summary>
   ///     Initializes a new instance of the <see cref="Game" /> class.
   /// </summary>
   public Game()
    {
    }

   /// <summary>
   ///     The current chess board state
   /// </summary>
   public Board Board { get; set; } = new();

    public static bool IsValidFen(string fen)
    {
        string board, side, castleRights, ep;

        // Split fen into its components
        var tokens = fen.Split(' ');
        switch (tokens.Length)
        {
            case 2:
                board = tokens[0];
                side = tokens[1];
                castleRights = "-";
                ep = "-";
                break;
            case 3:
                board = tokens[0];
                side = tokens[1];
                castleRights = tokens[2];
                ep = "-";
                break;
            case >= 4:
                board = tokens[0];
                side = tokens[1];
                castleRights = tokens[2];
                ep = tokens[3];
                break;
            default:
                return false;
        }

        // Let's check that all components of the supposed FEN are OK.
        if (side != "w" && side != "b")
            return false;
        if (castleRights != "-" && castleRights != "K" && castleRights != "Kk"
            && castleRights != "Kkq" && castleRights != "Kq" && castleRights != "KQ"
            && castleRights != "KQk" && castleRights != "KQq" && castleRights != "KQkq"
            && castleRights != "k" && castleRights != "q" && castleRights != "kq"
            && castleRights != "Q" && castleRights != "Qk" && castleRights != "Qq"
            && castleRights != "Qkq")
            return false;
        if (ep != "-")
        {
            if (ep.Length != 2)
                return false;
            if (!(ep[0] >= 'a' && ep[0] <= 'h'))
                return false;
            if (!((side == "w" && ep[1] == '6') || (side == "b" && ep[1] == '3')))
                return false;
        }

        // The tricky part: The board.
        // Seven slashes?
        if (board.Count(c => c == '/') != 7)
            return false;

        // Only legal characters?
        if (board.Any(c => c is not ('/' or >= '1' and <= '8')))
            return false;
        if (board.Any(c => PieceTypeFromChar(c) == PieceType.None))
            return false;

        // Exactly one king per side?
        if (board.Count(c => c == 'K') != 1)
            return false;
        if (board.Count(c => c == 'k') != 1)
            return false;

        // Are other piece counts reasonable?
        int wp, bp, wn, bn, wr, br, wb, bb, wq, bq;
        wp = board.Count(c => c == 'P');
        bp = board.Count(c => c == 'p');
        wn = board.Count(c => c == 'N');
        bn = board.Count(c => c == 'n');
        wb = board.Count(c => c == 'B');
        bb = board.Count(c => c == 'b');
        wr = board.Count(c => c == 'R');
        br = board.Count(c => c == 'r');
        wq = board.Count(c => c == 'Q');
        bq = board.Count(c => c == 'q');
        return wp > 8 || bp > 8 || wn > 10 || bn > 10 || wb > 10 || bb > 10 ||
               wr > 10 || br > 10 || wq > 9 || bq > 10 ||
               wp + wn + wb + wr + wq > 15 || bp + bn + bb + br + bq > 15;
    }

    public static Player PlayerFromChar(char c)
    {
        return c switch
        {
            'b' => Player.Black,
            'w' => Player.White,
            _ => char.IsLower(c) ? Player.Black : Player.White
        };
    }

    public static PieceType PieceTypeFromChar(char c)
    {
        var pieceChar = char.ToLower(c);
        return pieceChar switch
        {
            'p' => PieceType.Pawn,
            'n' => PieceType.Knight,
            'b' => PieceType.Bishop,
            'r' => PieceType.Rook,
            'q' => PieceType.Queen,
            'k' => PieceType.King,
            _ => PieceType.None
        };
    }

    /*
     fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
bool Position::is_valid_fen(const std::string &fen) {
   std::istringstream iss(fen);
   std::string board, side, castleRights, ep;

// 0

   if (!iss) return false;

   iss >> board;
// 1
   if (!iss) return false;

   iss >> side;
//2
   if (!iss) {
      castleRights = "-";
      ep = "-";
   } else {
      iss >> castleRights;
      //3
      if (iss)
         iss >> ep;
         //4
      else
         ep = "-";
   }

   // Let's check that all components of the supposed FEN are OK.
   if (side != "w" && side != "b") return false;
   if (castleRights != "-" && castleRights != "K" && castleRights != "Kk"
       && castleRights != "Kkq" && castleRights != "Kq" && castleRights !="KQ"
       && castleRights != "KQk" && castleRights != "KQq" && castleRights != "KQkq"
       && castleRights != "k" && castleRights != "q" && castleRights != "kq"
       && castleRights != "Q" && castleRights != "Qk" && castleRights != "Qq"
       && castleRights != "Qkq")
      return false;
   if (ep != "-") {
      if (ep.length() != 2) return false;
      if (!(ep[0] >= 'a' && ep[0] <= 'h')) return false;
      if (!((side == "w" && ep[1] == '6') || (side == "b" && ep[1] == '3')))
         return false;
   }

   // The tricky part: The board.
   // Seven slashes?
   if (std::count(board.begin(), board.end(), '/') != 7) return false;
   // Only legal characters?
   for (int i = 0; i < board.length(); i++)
      if (!(board[i] == '/' || (board[i] >= '1' && board[i] <= '8')
            || piece_type_is_ok(piece_type_from_char(board[i]))))
         return false;
   // Exactly one king per side?
   if (std::count(board.begin(), board.end(), 'K') != 1) return false;
   if (std::count(board.begin(), board.end(), 'k') != 1) return false;
   // Other piece counts reasonable?
   size_t wp = std::count(board.begin(), board.end(), 'P'),
      bp = std::count(board.begin(), board.end(), 'p'),
      wn = std::count(board.begin(), board.end(), 'N'),
      bn = std::count(board.begin(), board.end(), 'n'),
      wb = std::count(board.begin(), board.end(), 'B'),
      bb = std::count(board.begin(), board.end(), 'b'),
      wr = std::count(board.begin(), board.end(), 'R'),
      br = std::count(board.begin(), board.end(), 'r'),
      wq = std::count(board.begin(), board.end(), 'Q'),
      bq = std::count(board.begin(), board.end(), 'q');
   if (wp > 8 || bp > 8 || wn > 10 || bn > 10 || wb > 10 || bb > 10
       || wr > 10 || br > 10 || wq > 9 || bq > 10
       || wp + wn + wb + wr + wq > 15 || bp + bn + bb + br + bq > 15)
      return false;

   // OK, looks close enough to a legal position. Let's try to parse
   // the FEN and see!
   Position p;
   p.from_fen(board + " " + side + " " + castleRights + " " + ep);
   return p.is_ok(true);
}
     
     */
}
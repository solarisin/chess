namespace Solarisin.Chess.Shared.Models;

public class Position : IEqualityComparer<Position>
{
    private int _rank = 1;

    /// <summary>
    ///     Initialize a new instance of the <see cref="Position" /> class.
    /// </summary>
    public Position()
    {
    }

    /// <summary>
    ///     Initialize a new instance of the <see cref="Position" /> class.
    /// </summary>
    /// <param name="rank">The rank.</param>
    /// <param name="file">The file.</param>
    public Position(int rank, File file)
    {
        Rank = rank;
        File = file;
    }

    /// <summary>
    ///     Initialize a new instance of the <see cref="Position" /> class.
    /// </summary>
    /// <param name="identifier">The position identifier.</param>
    public Position(string identifier)
    {
        var newPosition = ToPosition(identifier);
        Rank = newPosition.Rank;
        File = newPosition.File;
    }

    /// <summary>
    ///     Retrieve the File of this position
    /// </summary>
    public File File { get; set; } = File.A;

    // Retrieve the Rank of this position
    public int Rank
    {
        get => _rank;
        set
        {
            if (value < 1 || value > 8)
                throw new ArgumentOutOfRangeException(nameof(value), "Rank must be between 1 and 8");
            _rank = value;
        }
    }

    /// <summary>
    ///     Retrieve the string identifier for this position.
    /// </summary>
    public string Identifier => ToIdentifier(this);

    /// <summary>
    ///     Determine if two positions refer to the same position.
    /// </summary>
    /// <param name="first">The first position</param>
    /// <param name="second">The second position</param>
    /// <returns>true, if the positions refer to the same position on the board.</returns>
    public bool Equals(Position? first, Position? second)
    {
        if (ReferenceEquals(first, second)) return true;
        if (ReferenceEquals(first, null)) return false;
        if (ReferenceEquals(second, null)) return false;
        if (first.GetType() != second.GetType()) return false;
        return first._rank == second._rank && first.File == second.File;
    }

    /// <summary>
    ///     Generate a hash code for the position.
    /// </summary>
    /// <param name="position">The position to generate a hash code for</param>
    /// <returns></returns>
    public int GetHashCode(Position position)
    {
        return HashCode.Combine(position._rank, (int)position.File);
    }

    /// <summary>
    ///     Convert a position string identifier to a position.
    /// </summary>
    /// <param name="identifier">The position identifier</param>
    /// <returns>The converted position</returns>
    public static Position ToPosition(string identifier)
    {
        var rank = identifier[0];
        var file = identifier[1].ToString().ToUpper();

        return new Position { Rank = rank - '0', File = Enum.Parse<File>(file) };
    }

    /// <summary>
    ///     Retrieve the string identifier for the given chess position
    /// </summary>
    /// <param name="position">The position</param>
    /// <returns>The converted position identifier</returns>
    public static string ToIdentifier(Position position)
    {
        return $"{position.Rank}{position.File}".ToLower();
    }

    /// <summary>
    ///     Get the difference between two chess positions (to-from)
    /// </summary>
    /// <param name="from">The starting position</param>
    /// <param name="to">The ending position</param>
    /// <returns>A tuple containing the rank and file difference between the two positions.</returns>
    public static (int RankDifference, int FileDifference) GetDifference(Position from, Position to)
    {
        var rankDifference = to.Rank - from.Rank;
        var fileDifference = to.File - from.File;
        return (rankDifference, fileDifference);
    }

    /// <summary>
    ///     Get the distance between two chess positions - abs(to-from)
    /// </summary>
    /// <param name="from">The starting position</param>
    /// <param name="to">The ending position</param>
    /// <returns>A tuple containing the rank and file distances between the two positions.</returns>
    public static (int RankDistance, int FileDistance) GetDistance(Position from, Position to)
    {
        var difference = GetDifference(from, to);
        var rankDistance = Math.Abs(difference.RankDifference);
        var fileDistance = Math.Abs(difference.FileDifference);
        return (rankDistance, fileDistance);
    }

    /// <summary>
    ///     Retrieve all chess positions between two positions (the movement "ray"
    /// </summary>
    /// <param name="startPosition">The starting position</param>
    /// <param name="endPosition">The ending position</param>
    /// <returns>A list of the positions</returns>
    public static List<Position> GetPositionsBetween(Position startPosition, Position endPosition)
    {
        // Verify the positions are in a straight line from one another and contain at least one position between them
        // and return the list of positions between them
        var positions = new List<Position>();
        var (rankDifference, fileDifference) = GetDifference(startPosition, endPosition);
        var (rankDistance, fileDistance) = GetDistance(startPosition, endPosition);
        var rankDirection = rankDifference != 0 ? rankDifference / Math.Abs(rankDifference) : 0;
        var fileDirection = fileDifference != 0 ? fileDifference / Math.Abs(fileDifference) : 0;
        if (rankDistance == 0 && fileDistance > 1)
            // Vertical
            for (var i = 1; i < fileDistance; i++)
                positions.Add(new Position(startPosition.Rank, startPosition.File + i * fileDirection));
        else if (fileDistance == 0 && rankDistance > 1)
            // Horizontal
            for (var i = 1; i < rankDistance; i++)
                positions.Add(new Position(startPosition.Rank + i * rankDirection, startPosition.File));
        else if (rankDistance == fileDistance && rankDistance > 1)
            // Diagonal
            for (var i = 1; i < rankDistance; i++)
                positions.Add(new Position(startPosition.Rank + i * rankDirection,
                    startPosition.File + i * fileDirection));
        else
            return new List<Position>();
        return positions;
    }

    /// <summary>
    ///     Function used to validate the rank and file of a chess position.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown if rank or file are out of range.</exception>
    public void Validate()
    {
        if (Rank is < 1 or > 8) throw new ArgumentException($"Invalid rank {Rank.ToString()}");
        if (File is < File.A or > File.H) throw new ArgumentException($"Invalid file {File}");
    }
}
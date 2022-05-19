namespace Solarisin.Chess.Shared.Models.Options;

/// <summary>
///     Encapsulates any options for the chess engine environment, describing how
///     to execute the engine on the host.
/// </summary>
public class EnvironmentOptions
{
    /// <summary>
    ///     Configuration section name for the options pattern
    /// </summary>
    public const string Section = "Environment";

    /// <summary>
    ///     Folder location of the uci-compatible engine executable
    /// </summary>
    public string Location { get; set; } = "/usr/local/bin";

    /// <summary>
    ///     Name of the executable, including any extension, to use when issuing uci commands
    /// </summary>
    public string ExecutableName { get; set; } = "stockfish";

    /// <summary>
    ///     Specifies whether the engine executable lies within the $PATH variable. If true, engine CLI calls will not include
    ///     the full location.
    /// </summary>
    public bool IsInPath { get; set; } = true;

    /// <summary>
    ///     Construct the process path as configured by these environment options.
    /// </summary>
    /// <returns></returns>
    public string BuildProcessPath()
    {
        return IsInPath ? ExecutableName : Path.Join(Location, ExecutableName);
    }
}
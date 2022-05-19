namespace Solarisin.Chess.Shared.Models.Options;

/// <summary>
///     Encapsulates any other static options for the chess engine
/// </summary>
public class EngineOptions
{
    /// <summary>
    ///     Configuration section name for the options pattern
    /// </summary>
    public const string Section = "Engine";

    /// <summary>
    ///     Specifies the uci engine implementation to use.
    /// </summary>
    public UciImplementation Implementation { get; set; } = UciImplementation.Stockfish;
}
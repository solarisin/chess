using System.Runtime.InteropServices;

namespace Solarisin.Chess.Shared.Models.Options;

/// <summary>
///     Implementation of the <see cref="IUciRunnerOptions" /> interface for the Stockfish engine runner.
/// </summary>
public class StockfishRunnerOptions : IUciRunnerOptions
{
    /// <inheritdoc />
    public string ProcessPath { get; set; } = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
        RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "stockfish.exe" : "stockfish");

    /// <inheritdoc />
    public UciRunnerAction Action { get; set; } = UciRunnerAction.Identify;

    /// <inheritdoc />
    public IList<string> CommandList { get; set; } =
        new List<string> { Interpreter.EnumToConsole(CommandId.UciInit) };

    /// <inheritdoc />
    public IList<ResponseId> IncludedResponses { get; set; } =
        new List<ResponseId> { ResponseId.Name, ResponseId.Author };

    /// <inheritdoc />
    public IList<ResponseId> ExcludedResponses { get; set; } = new List<ResponseId>();
}
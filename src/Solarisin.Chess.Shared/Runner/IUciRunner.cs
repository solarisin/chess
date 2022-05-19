using Solarisin.Chess.Shared.Models.Options;

namespace Solarisin.Chess.Shared.Runner;

/// <summary>
///     Interface for running uci engine commands and retrieving a filtered list of responses.
/// </summary>
public interface IUciRunner
{
    /// <summary>
    ///     The runner options used to execute the engine.
    /// </summary>
    public IUciRunnerOptions Options { get; set; }

    /// <summary>
    ///     Run the engine with the supplied options.
    /// </summary>
    /// <returns>The engine output separated by lines.</returns>
    public List<string> Run();

    /// <summary>
    ///     Run the engine asynchronously with the supplied options.
    /// </summary>
    /// <param name="ctx">The async cancellation token.</param>
    /// <returns>The engine output separated by lines.</returns>
    public Task<List<string>> RunAsync(CancellationToken ctx = default);

    /// <summary>
    ///     Validate the supplied options and return a reason if they are not valid
    /// </summary>
    /// <param name="reason">Reason why the options are not valid</param>
    /// <returns>True if the options are valid, false otherwise</returns>
    public bool ValidateOptions(out string reason);
}
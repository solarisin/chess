namespace Solarisin.Chess.Shared.Models.Options;

/// <summary>
///     Builder interface for the <see cref="IUciRunnerOptions" /> interface.
/// </summary>
public interface IUciRunnerOptionsBuilder
{
    /// <summary>
    ///     Use the specified process path to run the uci engine.
    /// </summary>
    /// <param name="processPath">The path to the process to run.</param>
    /// <returns>The builder instance.</returns>
    public IUciRunnerOptionsBuilder UseProcessPath(string processPath);

    /// <summary>
    ///     Execute the given <see cref="UciRunnerAction" /> on the engine.
    /// </summary>
    /// <param name="action">The logical chess action to execute.</param>
    /// <returns>The builder instance.</returns>
    public IUciRunnerOptionsBuilder AssignAction(UciRunnerAction action);
}
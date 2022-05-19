namespace Solarisin.Chess.Shared.Models.Options;

/// <summary>
///     Model interface containing required uci runner options
/// </summary>
public interface IUciRunnerOptions
{
    /// <summary>
    ///     The path to the uci engine process to run.
    /// </summary>
    public string ProcessPath { get; set; }

    /// <summary>
    ///     The logical action to perform using the uci engine.
    /// </summary>
    public UciRunnerAction Action { get; set; }

    /// <summary>
    ///     The list of commands to execute on the engine.
    /// </summary>
    public IList<string> CommandList { get; set; }

    /// <summary>
    ///     Specifies the responses that should be included by the runner. Overrides
    ///     ExcludedResponses.
    /// </summary>
    public IList<ResponseId> IncludedResponses { get; set; }

    /// <summary>
    ///     Specifies the responses that should be excluded by the runner. If any
    ///     IncludedResponses are specified, this list is ignored.
    /// </summary>
    public IList<ResponseId> ExcludedResponses { get; set; }
}
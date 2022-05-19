namespace Solarisin.Chess.Shared.Models.Options;

/// <summary>
///     Builder for the <see cref="StockfishRunnerOptions" /> class.
/// </summary>
public class StockfishRunnerOptionsBuilder : IUciRunnerOptionsBuilder
{
    public StockfishRunnerOptionsBuilder(IUciRunnerOptions? options = default)
    {
        Options = options as StockfishRunnerOptions ?? new StockfishRunnerOptions();
    }

    public StockfishRunnerOptions Options { get; }

    public IUciRunnerOptionsBuilder UseProcessPath(string processPath)
    {
        Options.ProcessPath = processPath;
        return this;
    }

    public IUciRunnerOptionsBuilder AssignAction(UciRunnerAction action)
    {
        Options.Action = action;

        // Set the default command lists and included/excluded responses for each action
        switch (action)
        {
            case UciRunnerAction.Identify:
                var identifyCmd = Interpreter.EnumToConsole(CommandId.UciInit);
                Options.CommandList = new List<string> { identifyCmd };
                Options.IncludedResponses = new List<ResponseId> { ResponseId.Name, ResponseId.Author };
                break;
            case UciRunnerAction.ListOptions:
                var listOptionsCmd = Interpreter.EnumToConsole(CommandId.UciInit);
                Options.CommandList = new List<string> { listOptionsCmd };
                Options.IncludedResponses = new List<ResponseId> { ResponseId.Option };
                break;
            case UciRunnerAction.BestMove:
                throw new NotImplementedException("BestMove action not implemented yet");
            case UciRunnerAction.BestMoveTimed:
                throw new NotImplementedException("BestMoveTimed action not implemented yet");
            default:
                throw new ArgumentOutOfRangeException(nameof(action), action, null);
        }

        return this;
    }

    public StockfishRunnerOptions Build()
    {
        return Options;
    }
}
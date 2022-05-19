using Solarisin.Chess.Shared.Models.Options;
using Solarisin.Chess.Shared.Runner;

namespace Solarisin.Chess.Shared.Services;

public class StockfishRunnerService : IUciRunnerService
{
    public Task<List<string>> RunAsync(IUciRunnerOptions options, CancellationToken ctx = default)
    {
        var runner = new StockfishRunner(options);
        return runner.RunAsync(ctx);
    }
}
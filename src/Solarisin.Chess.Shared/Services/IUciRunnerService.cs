using Solarisin.Chess.Shared.Models.Options;

namespace Solarisin.Chess.Shared.Services;

public interface IUciRunnerService
{
    public Task<List<string>> RunAsync(IUciRunnerOptions options, CancellationToken ctx = default);
}
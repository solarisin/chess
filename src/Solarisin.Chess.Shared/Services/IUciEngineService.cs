using Solarisin.Chess.Shared.Models.Options;
using Solarisin.Chess.Shared.Models.Uci;
using Solarisin.Chess.Shared.Models.Uci.Options;

namespace Solarisin.Chess.Shared.Services;

public interface IUciEngineService
{
    public EnvironmentOptions EnvironmentOptions { get; set; }

    public EngineOptions EngineOptions { get; set; }

    public Task<EngineIdentity> Identify();

    public Task<List<IUciOption>> ListOptions();

    public void Shutdown();
}
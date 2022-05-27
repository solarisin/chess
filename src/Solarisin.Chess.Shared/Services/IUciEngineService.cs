using Solarisin.Chess.Shared.Models.Options;
using Solarisin.Chess.Shared.Models.Uci;
using Solarisin.Chess.Shared.Models.Uci.Options;

namespace Solarisin.Chess.Shared.Services;

/// <summary>
/// Interface to the uci engine service containing the methods to communicate with the engine
/// </summary>
public interface IUciEngineService
{
    /// <summary>
    /// Identify the uci engine
    /// </summary>
    /// <returns>The identity of the engine</returns>
    public Task<EngineIdentity> Identify();

    /// <summary>
    /// Enumerate the available options for the engine
    /// </summary>
    /// <returns>The available uci options and their values</returns>
    public Task<List<IUciOption>> ListOptions();
}
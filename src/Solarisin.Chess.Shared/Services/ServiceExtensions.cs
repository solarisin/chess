using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Solarisin.Chess.Shared.Models.Options;

namespace Solarisin.Chess.Shared.Services;

public static class ServiceExtensions
{
    public static IServiceCollection AddUciServices(this IServiceCollection services, IConfiguration configuration)
    {
        var environmentSection = configuration.GetSection(EnvironmentOptions.Section);
        var engineSection = configuration.GetSection(EngineOptions.Section);
        var engineOptions = engineSection.Get<EngineOptions>()
                            ?? throw new InvalidOperationException($"'EngineOptions' configuration section not found.");

        // Configure the IOptions interfaces with the settings
        services.Configure<EnvironmentOptions>(environmentSection);
        services.Configure<EngineOptions>(engineSection);

        switch (engineOptions.Implementation)
        {
            case UciImplementation.Stockfish:
                services.AddSingleton<IUciRunnerService, StockfishRunnerService>();
                services.AddSingleton<IUciEngineService, StockfishEngineService>();
                break;
            default:
                throw new NotSupportedException($"Unsupported UCI engine '{engineOptions.Implementation}'");
        }

        return services;
    }
}
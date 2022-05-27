using Microsoft.Extensions.Options;
using Serilog;
using Solarisin.Chess.Shared.Models.Options;
using Solarisin.Chess.Shared.Models.Uci;
using Solarisin.Chess.Shared.Models.Uci.Options;
using Solarisin.Core.Extensions;

namespace Solarisin.Chess.Shared.Services;

/// <summary>
///     Implementation of the <see cref="IUciEngineService" /> interface using the stockfish engine
/// </summary>
public class StockfishEngineService : IUciEngineService
{
    /// <summary>
    ///     Constructor for an instance of the <see cref="StockfishEngineService" /> class
    /// </summary>
    /// <param name="runner">The runner service used to run the stockfish engine</param>
    /// <param name="environmentOptions">The environment options</param>
    /// <param name="engineOptions">The engine options</param>
    public StockfishEngineService(IUciRunnerService runner,
        IOptions<EnvironmentOptions> environmentOptions,
        IOptions<EngineOptions> engineOptions)
    {
        EnvironmentOptions = environmentOptions.Value;
        EngineOptions = engineOptions.Value;
        UciRunnerService = runner;
    }

    private IUciRunnerService UciRunnerService { get; }
    private EnvironmentOptions EnvironmentOptions { get; }
    private EngineOptions EngineOptions { get; }

    /// <summary>
    ///     Identify the stockfish engine
    /// </summary>
    /// <returns>The identity information of the stockfish engine</returns>
    /// <exception cref="InvalidOperationException">If the engine environment configuration is invalid</exception>
    public async Task<EngineIdentity> Identify()
    {
        /*
        Stockfish 15 by the Stockfish developers (see AUTHORS file)
        id name Stockfish 15
        id author the Stockfish developers (see AUTHORS file)

        option name Debug Log File type string default 
        option name Threads type spin default 1 min 1 max 512
        option name Hash type spin default 16 min 1 max 33554432
        option name Clear Hash type button
        option name Ponder type check default false
        option name MultiPV type spin default 1 min 1 max 500
        option name Skill Level type spin default 20 min 0 max 20
        option name Move Overhead type spin default 10 min 0 max 5000
        option name Slow Mover type spin default 100 min 10 max 1000
        option name nodestime type spin default 0 min 0 max 10000
        option name UCI_Chess960 type check default false
        option name UCI_AnalyseMode type check default false
        option name UCI_LimitStrength type check default false
        option name UCI_Elo type spin default 1350 min 1350 max 2850
        option name UCI_ShowWDL type check default false
        option name SyzygyPath type string default <empty>
        option name SyzygyProbeDepth type spin default 1 min 1 max 100
        option name Syzygy50MoveRule type check default true
        option name SyzygyProbeLimit type spin default 7 min 0 max 7
        option name Use NNUE type check default true
        option name EvalFile type string default nn-6877cd24400e.nnue
        uciok
        */

        List<string> lines;
        try
        {
            var builder = new StockfishRunnerOptionsBuilder();
            builder
                .AssignAction(UciRunnerAction.Identify)
                .UseProcessPath(EnvironmentOptions.BuildProcessPath());
            lines = await UciRunnerService.RunAsync(builder.Build());
        }
        catch (InvalidOperationException e)
        {
            throw new InvalidOperationException("Environment configuration is invalid", e);
        }

        lines.PopFirst();

        // Decode each line, should be only id after the filter
        EngineIdentity id = new();
        var nameId = Interpreter.EnumToConsole(ResponseId.Name);
        var authorId = Interpreter.EnumToConsole(ResponseId.Author);
        foreach (var line in lines)
            if (line.StartsWith(nameId))
            {
                id.Name = line[(nameId.Length + 1)..];
                Log.ForContext<StockfishEngineService>()
                    .Information("Parsed {Console} '{Value}' from option string: '{OptionText}'",
                        nameId, id.Name, line);
            }
            else if (line.StartsWith(authorId))
            {
                id.Author = line[(authorId.Length + 1)..];
                Log.ForContext<StockfishEngineService>()
                    .Information("Parsed {Console} '{Value}' from option string: '{OptionText}'",
                        authorId, id.Author, line);
            }
            else
            {
                Log.ForContext<StockfishEngineService>()
                    .ForContext("ResponseString", line)
                    .Warning("Unrecognized and unhandled response from stockfish");
            }

        return id;
    }

    /// <summary>
    ///     List the available uci options and their values within the stockfish engine
    /// </summary>
    /// <returns>The available stockfish uci options</returns>
    public async Task<List<IUciOption>> ListOptions()
    {
        var lines = await ExecuteRunnerAction(UciRunnerAction.ListOptions);
        lines.PopFirst();

        // Decode each line, should be only lines starting with option after the runner filter
        List<IUciOption> options = new();
        var optionId = Interpreter.EnumToConsole(ResponseId.Option);
        foreach (var line in lines)
            if (line.StartsWith(optionId))
                try
                {
                    var option = IUciOption.CreateOption(line);
                    options.Add(option);
                    Log.ForContext<StockfishEngineService>()
                        .Information("Parsed {Console} '{Value}' from option string: '{OptionText}'",
                            optionId, option, line);
                }
                catch (ArgumentOutOfRangeException e)
                {
                    Log.ForContext<StockfishEngineService>()
                        .ForContext("OptionString", line)
                        .Error(e, "Failed to create option from option string");
                }
            else
                Log.ForContext<StockfishEngineService>()
                    .ForContext("ResponseString", line)
                    .Warning("Unrecognized and unhandled response string from stockfish");

        return options;
    }

    /// <summary>
    ///     Execute the given runner action on the stockfish runner and return the list of output lines
    /// </summary>
    /// <param name="action">The action to execute</param>
    /// <returns>The list of output lines</returns>
    /// <exception cref="InvalidOperationException">If the engine environment configuration is invalid</exception>
    private Task<List<string>> ExecuteRunnerAction(UciRunnerAction action)
    {
        try
        {
            var builder = new StockfishRunnerOptionsBuilder();
            builder
                .AssignAction(action)
                .UseProcessPath(EnvironmentOptions.BuildProcessPath());
            return UciRunnerService.RunAsync(builder.Build());
        }
        catch (InvalidOperationException e)
        {
            throw new InvalidOperationException("Environment configuration is invalid", e);
        }
    }
}
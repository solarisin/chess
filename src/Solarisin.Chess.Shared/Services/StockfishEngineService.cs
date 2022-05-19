using Microsoft.Extensions.Options;
using Serilog;
using Solarisin.Chess.Shared.Models.Options;
using Solarisin.Chess.Shared.Models.Uci;
using Solarisin.Chess.Shared.Models.Uci.Options;
using Solarisin.Core.Extensions;

namespace Solarisin.Chess.Shared.Services;

public class StockfishEngineService : IUciEngineService
{
    public StockfishEngineService(IUciRunnerService runner, IOptions<EnvironmentOptions> environmentOptions,
        IOptions<EngineOptions> engineOptions)
    {
        EnvironmentOptions = environmentOptions.Value;
        EngineOptions = engineOptions.Value;
        UciRunnerService = runner;
    }

    public IUciRunnerService UciRunnerService { get; }
    public EnvironmentOptions EnvironmentOptions { get; set; }
    public EngineOptions EngineOptions { get; set; }

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

        // Parse the response
        // break the response into lines
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

    public Task<List<IUciOption>> ListOptions()
    {
        throw new NotImplementedException();

        /*
        var lines = response.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
        lines.PopFirst();
        
        // Decode each line, should be either id, option or uciok
        EngineIdentity id = new();
        var name = Interpreter.EnumToConsole(ResponseId.Name);
        var author = Interpreter.EnumToConsole(ResponseId.Author);
        var option = Interpreter.EnumToConsole(ResponseId.Option);
        foreach (var line in lines)
        {
            if (line.StartsWith(name))
            {
                id.Name = line[name.Length..];
                Log.ForContext<UciService>().Information("Parsed {Console} '{Value}' from option string: '{OptionText}'", name, id.Name, line);
            }
            else if (line.StartsWith(author))
            {
                id.Author = line[author.Length..];
                Log.ForContext<UciService>().Information("Parsed {Console} '{Value}' from option string: '{OptionText}'", name, id.Author, line);
            }
            else if (line.StartsWith(option))
            {
                var createdOption = IUciOption.CreateOption(line);
                if (createdOption != null)
                {
                    InterfaceOptions.UciOptions.Add(createdOption);
                    Log.ForContext<UciService>()
                        .ForContext("CreatedOption", createdOption, true)
                        .Information("Parsed {Console} '{Value}' from option string: '{OptionText}'", option, createdOption.Name, line);
                }
                else
                    Log.ForContext<UciService>().Error("Failed to create option from uci response string: '{OptionText}'", line);
            }
            else if (line.StartsWith(Interpreter.EnumToConsole(ResponseId.UciOk)))
            {
                result = true;
                break;
            }
        }
        return result;
        */
    }

    public void Shutdown()
    {
        throw new NotImplementedException();
    }
}
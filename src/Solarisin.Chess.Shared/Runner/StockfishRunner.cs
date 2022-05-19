using System.ComponentModel;
using System.Text;
using CliWrap;
using CliWrap.Buffered;
using Serilog;
using Solarisin.Chess.Shared.Models.Options;
using Solarisin.Core.Extensions.Logging;

namespace Solarisin.Chess.Shared.Runner;

/// <summary>
///     Implements the <see cref="IUciRunner" /> interface to execute commands on the stockfish chess engine.
/// </summary>
public class StockfishRunner : IUciRunner
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="StockfishRunner" /> class with default options.
    /// </summary>
    public StockfishRunner()
    {
        Options = new StockfishRunnerOptions();
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="StockfishRunner" /> class.
    /// </summary>
    /// <param name="options">The uci runner options to utilize.</param>
    public StockfishRunner(IUciRunnerOptions options)
    {
        Options = options;
    }

    /// <inheritdoc />
    public IUciRunnerOptions Options { get; set; }

    /// <inheritdoc />
    public List<string> Run()
    {
        throw new NotImplementedException("Run is not implemented yet");
    }

    /// <inheritdoc />
    public async Task<List<string>> RunAsync(CancellationToken ctx = default)
    {
        if (!ValidateOptions(out var reason))
        {
            Log.ForContext<StockfishRunner>().Here()
                .ForContext("RunnerOptions", Options, true)
                .Error("Runner options are invalid: {Reason}", reason);
            throw new InvalidOperationException(reason);
        }

        var inputBuilder = new StringBuilder();
        foreach (var command in Options.CommandList)
            inputBuilder.AppendLine(command.Trim().ReplaceLineEndings(Environment.NewLine));

        var commandString = inputBuilder.ToString();
        BufferedCommandResult result;
        try
        {
            result = await Cli.Wrap(Options.ProcessPath)
                .WithStandardInputPipe(PipeSource.FromString(commandString))
                .ExecuteBufferedAsync(ctx);
        }
        catch (Win32Exception e)
        {
            // Win32Exception is thrown when the engine is not found
            Log.ForContext<StockfishRunner>().Here()
                .ForContext("ProcessPath", Options.ProcessPath)
                .ForContext("CommandString", commandString)
                .Error(e, "Failed to execute CliWrap");
            throw; // indicate unrecoverable error
        }
        catch (OperationCanceledException)
        {
            // OperationCanceledException is thrown when the operation is canceled
            // using the CancellationToken
            Log.ForContext<StockfishRunner>()
                .ForContext("ProcessPath", Options.ProcessPath)
                .ForContext("CommandString", commandString)
                .Information("Runner operation was canceled");
            return new List<string>();
        }

        var exitCode = result.ExitCode;
        var stdOut = result.StandardOutput;
        var stdErr = result.StandardError;

        // TODO handle wrong exit code / output on stderr

        // TODO filter included/excluded lines

        return stdOut.Split(Environment.NewLine).ToList();
    }

    /// <inheritdoc />
    public bool ValidateOptions(out string reason)
    {
        reason = string.Empty;
        if (string.IsNullOrWhiteSpace(Options.ProcessPath))
        {
            reason = "ProcessPath is not set";
            return false;
        }

        if (!File.Exists(Options.ProcessPath))
        {
            reason = $"Stockfish does not exist at '{Options.ProcessPath}'";
            return false;
        }

        if (string.IsNullOrWhiteSpace(Options.CommandList.FirstOrDefault()))
        {
            reason = "CommandList is empty";
            return false;
        }

        return true;
    }
}
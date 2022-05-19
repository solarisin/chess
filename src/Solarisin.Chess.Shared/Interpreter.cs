namespace Solarisin.Chess.Shared;

/// <summary>
///     Class used to interpret the uci console text into logical commands.
/// </summary>
public static class Interpreter
{
    private static readonly Dictionary<OptionType, string> OptionTypeMap = new()
    {
        { OptionType.Check, "check" },
        { OptionType.Spin, "spin" },
        { OptionType.Combo, "combo" },
        { OptionType.Button, "button" },
        { OptionType.String, "string" }
    };

    private static readonly Dictionary<CommandId, string> CommandIdMap = new()
    {
        { CommandId.UciInit, "uci" },
        { CommandId.Debug, "debug" },
        { CommandId.IsReady, "isready" },
        { CommandId.SetOption, "setoption" },
        { CommandId.Register, "register" },
        { CommandId.UciNewGame, "ucinewgame" },
        { CommandId.Position, "position" },
        { CommandId.Go, "go" },
        { CommandId.Stop, "stop" },
        { CommandId.PonderHit, "ponderhit" },
        { CommandId.Quit, "quit" }
    };

    private static readonly Dictionary<ResponseId, string> ResponseIdMap = new()
    {
        { ResponseId.Name, "id name" },
        { ResponseId.Author, "id author" },
        { ResponseId.Option, "option" },
        { ResponseId.UciOk, "uciok" },
        { ResponseId.ReadyOk, "readyok" }
    };

    /// <summary>
    ///     Convert the given uci enumeration value to its console string representation.
    /// </summary>
    /// <param name="value">The uci enumeration value to convert.</param>
    /// <typeparam name="T">The type of the uci enumeration value.</typeparam>
    /// <returns>The console string representation of the uci enumeration value.</returns>
    /// <exception cref="ArgumentException">Thrown if the given uci enumeration value is not supported.</exception>
    /// <exception cref="InvalidCastException">Thrown if the given uci enumeration type is not supported.</exception>
    public static string EnumToConsole<T>(T value) where T : Enum
    {
        var underlyingType = Enum.GetUnderlyingType(value.GetType());
        var underlyingValue = Convert.ChangeType(value, underlyingType) as int? ?? -1;
        if (underlyingValue == -1)
            throw new ArgumentException("Enum value not found", nameof(value));

        var consoleString = "";
        if (typeof(T) == typeof(OptionType))
            consoleString = OptionTypeMap[(OptionType)underlyingValue];
        else if (typeof(T) == typeof(CommandId))
            consoleString = CommandIdMap[(CommandId)underlyingValue];
        else if (typeof(T) == typeof(ResponseId))
            consoleString = ResponseIdMap[(ResponseId)underlyingValue];
        else
            throw new InvalidCastException($"{typeof(T).Name} is not a valid console type.");
        return consoleString;
    }

    /// <summary>
    ///     Convert the given console string representation to its uci enumeration value.
    /// </summary>
    /// <param name="consoleCommand">The console command string to convert.</param>
    /// <typeparam name="T">The type of the uci enumeration value.</typeparam>
    /// <returns>The uci enumeration value.</returns>
    /// <exception cref="ArgumentException">Thrown if the given console command is empty.</exception>
    /// <exception cref="InvalidCastException">Thrown if the given console command type is not supported.</exception>
    public static T ConsoleToEnum<T>(string consoleCommand) where T : Enum
    {
        if (!consoleCommand.Any())
            throw new ArgumentException("Command cannot be empty.", nameof(consoleCommand));

        object type;
        if (typeof(T) == typeof(OptionType))
            type = OptionTypeMap.FirstOrDefault(x => x.Value == consoleCommand).Key;
        else if (typeof(T) == typeof(CommandId))
            type = CommandIdMap.FirstOrDefault(x => x.Value == consoleCommand).Key;
        else if (typeof(T) == typeof(ResponseId))
            type = ResponseIdMap.FirstOrDefault(x => x.Value == consoleCommand).Key;
        else
            throw new InvalidCastException($"{typeof(T).Name} is not a valid console type.");
        return (T)type;
    }
}
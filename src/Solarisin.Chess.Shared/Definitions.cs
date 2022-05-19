namespace Solarisin.Chess.Shared;

/// <summary>
///     Enumeration of the possible option types in the UCI protocol.
/// </summary>
public enum OptionType
{
    Check,
    Spin,
    Combo,
    Button,
    String
}

/// <summary>
///     Logical action to be taken by the engine.
/// </summary>
public enum UciRunnerAction
{
    Identify,
    ListOptions,
    BestMove,
    BestMoveTimed
}

/// <summary>
///     Enumeration containing the possible commands to be sent to a UCI engine.
/// </summary>
public enum CommandId
{
    UciInit,
    Debug,
    IsReady,
    SetOption,
    Register,
    UciNewGame,
    Position,
    Go,
    Stop,
    PonderHit,
    Quit
}

/// <summary>
///     Enumeration containing the possible responses received from a uci engine.
/// </summary>
public enum ResponseId
{
    Name,
    Author,
    Option,
    UciOk,
    ReadyOk
}

/// <summary>
///     Enumeration of the supported uci engine implementations.
/// </summary>
public enum UciImplementation
{
    Stockfish
}
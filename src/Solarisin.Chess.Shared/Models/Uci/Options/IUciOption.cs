namespace Solarisin.Chess.Shared.Models.Uci.Options;

public interface IUciOption
{
    public string Name { get; set; }

    public OptionType Type { get; }

    public string Default { get; }

    public string Value { get; set; }

    public static IUciOption? CreateOption(string optionString)
    {
        // examples:
        // option name Debug Log File type string default
        // option name Threads type spin default 1 min 1 max 512
        // option name Hash type spin default 16 min 1 max 33554432
        // option name EvalFile type string default nn-6877cd24400e.nnue
        // option name UCI_Chess960 type check default false
        // option name Style type combo default Normal var Solid var Normal var Risky

        var words = optionString.Split(' ').ToList();

        // name and type are mandatory
        // Retrieve the name of the option, multiple words possible
        var nameArray = words.SkipWhile(s => s != "name").Skip(1).TakeWhile(s => s != "type");
        var name = string.Join(" ", nameArray);

        // Retrieve the type of the option, only one word
        var typeString = words.SkipWhile(s => s != "type").Skip(1).Take(1).ToArray()[0];
        var type = Interpreter.ConsoleToEnum<OptionType>(typeString);

        IUciOption option;
        string defaultString;
        switch (type)
        {
            case OptionType.Check:
                defaultString = words.SkipWhile(s => s != "default").Skip(1).Take(1).ToArray()[0];
                var defaultCheck = bool.Parse(defaultString);
                option = new CheckOption(name, defaultCheck);
                break;
            case OptionType.Spin:
                defaultString = words.SkipWhile(s => s != "default").Skip(1).Take(1).ToArray()[0];
                var defaultSpin = int.Parse(defaultString);
                var minString = words.SkipWhile(s => s != "min").Skip(1).Take(1).ToArray()[0];
                var maxString = words.SkipWhile(s => s != "max").Skip(1).Take(1).ToArray()[0];
                var min = int.Parse(minString);
                var max = int.Parse(maxString);
                option = new SpinOption(name, defaultSpin, min, max);
                break;
            case OptionType.Combo:
                defaultString = words.SkipWhile(s => s != "default").Skip(1).Take(1).ToArray()[0];
                var possibleValues = new List<string>();
                for (var i = 0; i < words.Count; i++)
                    if (words[i] == "var")
                        possibleValues.Add(words[i + 1]);
                option = new ComboOption(name, defaultString, possibleValues);
                break;
            case OptionType.String:
                defaultString = words.SkipWhile(s => s != "default").Skip(1).Take(1).ToArray()[0];
                option = new StringOption(name, defaultString);
                break;
            case OptionType.Button:
                option = new ButtonOption(name);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(optionString),
                    "invalid option string type, or failed to parse");
        }

        return option;
    }
}
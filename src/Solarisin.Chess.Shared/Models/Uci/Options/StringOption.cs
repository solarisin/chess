namespace Solarisin.Chess.Shared.Models.Uci.Options;

public class StringOption : IUciOption
{
    public StringOption(string name, string defaultValue)
    {
        Name = name;
        Type = OptionType.String;
        Value = defaultValue;
        Default = defaultValue;
    }

    public string Name { get; set; }

    public OptionType Type { get; }

    public string Value { get; set; }

    public string Default { get; }
}
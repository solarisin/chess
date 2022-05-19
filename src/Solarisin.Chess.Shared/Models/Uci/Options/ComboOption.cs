namespace Solarisin.Chess.Shared.Models.Uci.Options;

public class ComboOption : IUciOption
{
    public ComboOption(string name, string defaultValue, List<string> possibleValues)
    {
        if (possibleValues.Find(x => x == defaultValue) == null)
            throw new ArgumentException("Default value must be one of the possible values");

        Name = name;
        Type = OptionType.Combo;
        Value = defaultValue;
        Default = defaultValue;
        PossibleValues = possibleValues;
    }

    public List<string> PossibleValues { get; }
    public string Name { get; set; }

    public OptionType Type { get; }

    public string Value { get; set; }

    public string Default { get; }
}
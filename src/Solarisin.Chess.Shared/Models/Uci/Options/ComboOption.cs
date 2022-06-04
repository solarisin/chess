using System.Text.Json.Serialization;

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

    [JsonPropertyOrder(5)]
    public List<string> PossibleValues { get; }

    [JsonPropertyOrder(1)]
    public string Name { get; set; }

    [JsonPropertyOrder(2)]
    public OptionType Type { get; }

    [JsonPropertyOrder(3)]
    public string Value { get; set; }

    [JsonPropertyOrder(4)]
    public string Default { get; }
}
using System.Text.Json.Serialization;

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

    [JsonPropertyOrder(1)]
    public string Name { get; set; }

    [JsonPropertyOrder(2)]
    public OptionType Type { get; }

    [JsonPropertyOrder(3)]
    public string Value { get; set; }

    [JsonPropertyOrder(4)]
    public string Default { get; }
}
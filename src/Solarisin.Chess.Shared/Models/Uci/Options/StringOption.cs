using Newtonsoft.Json;

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

    [JsonProperty(Order = 1)] public string Name { get; set; }

    [JsonProperty(Order = 2)] public OptionType Type { get; }

    [JsonProperty(Order = 3)] public string Value { get; set; }

    [JsonProperty(Order = 4)] public string Default { get; }
}
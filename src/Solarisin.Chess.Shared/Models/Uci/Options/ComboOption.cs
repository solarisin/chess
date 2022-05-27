using Newtonsoft.Json;

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

    [JsonProperty(Order = 5)] public List<string> PossibleValues { get; }

    [JsonProperty(Order = 1)] public string Name { get; set; }

    [JsonProperty(Order = 2)] public OptionType Type { get; }

    [JsonProperty(Order = 3)] public string Value { get; set; }

    [JsonProperty(Order = 4)] public string Default { get; }
}
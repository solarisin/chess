using Newtonsoft.Json;

namespace Solarisin.Chess.Shared.Models.Uci.Options;

public class SpinOption : IUciOption
{
    public SpinOption(string name, int defaultValue, int min, int max)
    {
        Name = name;
        Type = OptionType.Spin;
        DefaultSpin = defaultValue;
        Spin = defaultValue;
        Min = min;
        Max = max;
    }

    [JsonProperty(Order = 3)]
    [JsonPropertyName("Value")]
    public int Spin { get; set; }

    [JsonProperty(Order = 4)]
    [JsonPropertyName("DefaultValue")]
    public int DefaultSpin { get; }

    [JsonProperty(Order = 5)] public int Min { get; }

    [JsonProperty(Order = 6)] public int Max { get; }

    [JsonProperty(Order = 1)] public string Name { get; set; }

    [JsonProperty(Order = 2)] public OptionType Type { get; }

    [Newtonsoft.Json.JsonIgnore]
    public string Value
    {
        get => Spin.ToString();
        set => Spin = int.Parse(value);
    }

    [Newtonsoft.Json.JsonIgnore] public string Default => DefaultSpin.ToString();
}
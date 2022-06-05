using System.Text.Json.Serialization;

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

    [JsonPropertyName("Value")]
    [JsonPropertyOrder(3)]
    public int Spin { get; set; }

    [JsonPropertyName("DefaultValue")]
    [JsonPropertyOrder(4)]
    public int DefaultSpin { get; }

    [JsonPropertyOrder(5)]
    public int Min { get; }

    [JsonPropertyOrder(6)]
    public int Max { get; }

    [JsonPropertyOrder(1)]
    public string Name { get; set; }

    [JsonPropertyOrder(2)]
    public OptionType Type { get; }

    [JsonIgnore]
    public string Value
    {
        get => Spin.ToString();
        set => Spin = int.Parse(value);
    }

    [JsonIgnore] public string Default => DefaultSpin.ToString();
}
using System.Text.Json.Serialization;

namespace Solarisin.Chess.Shared.Models.Uci.Options;

public class ButtonOption : IUciOption
{
    public ButtonOption(string name)
    {
        Name = name;
        Type = OptionType.Button;
    }

    [JsonPropertyOrder(1)] public string Name { get; set; }

    [JsonPropertyOrder(2)] public OptionType Type { get; set; }

    [JsonIgnore]
    public string Value
    {
        get => "";
        set => throw new InvalidOperationException("Button options do not have a value");
    }

    [JsonIgnore] public string Default => "";
}
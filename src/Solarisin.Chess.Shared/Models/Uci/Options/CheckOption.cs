using System.Text.Json.Serialization;

namespace Solarisin.Chess.Shared.Models.Uci.Options;

public class CheckOption : IUciOption
{
    public CheckOption(string name, bool defaultValue)
    {
        Name = name;
        Type = OptionType.Check;
        DefaultChecked = defaultValue;
        Checked = defaultValue;
    }

    [JsonPropertyOrder(3)]
    [JsonPropertyName("Value")]
    public bool Checked { get; set; }

    [JsonPropertyOrder(4)]
    [JsonPropertyName("DefaultValue")]
    public bool DefaultChecked { get; private set; }

    [JsonPropertyOrder(1)]
    public string Name { get; set; }

    [JsonPropertyOrder(2)]
    public OptionType Type { get; set; }

    [JsonIgnore]
    public string Value
    {
        get => Checked.ToString();
        set => Checked = bool.Parse(value);
    }

    [JsonIgnore]
    public string Default
    {
        get => DefaultChecked.ToString();
        set => DefaultChecked = bool.Parse(value);
    }
}
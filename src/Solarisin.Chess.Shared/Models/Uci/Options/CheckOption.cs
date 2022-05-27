using System.Text.Json.Serialization;
using Newtonsoft.Json;

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

    [JsonProperty(Order = 3)]
    [JsonPropertyName("Value")]
    public bool Checked { get; set; }

    [JsonProperty(Order = 4)]
    [JsonPropertyName("DefaultValue")]
    public bool DefaultChecked { get; private set; }

    [JsonProperty(Order = 1)] public string Name { get; set; }

    [JsonProperty(Order = 2)] public OptionType Type { get; set; }

    [Newtonsoft.Json.JsonIgnore]
    public string Value
    {
        get => Checked.ToString();
        set => Checked = bool.Parse(value);
    }

    [Newtonsoft.Json.JsonIgnore]
    public string Default
    {
        get => DefaultChecked.ToString();
        set => DefaultChecked = bool.Parse(value);
    }
}
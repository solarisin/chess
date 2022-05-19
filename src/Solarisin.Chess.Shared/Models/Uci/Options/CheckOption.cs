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

    public bool Checked { get; set; }

    public bool DefaultChecked { get; private set; }
    public string Name { get; set; }

    public OptionType Type { get; set; }

    public string Value
    {
        get => Checked.ToString();
        set => Checked = bool.Parse(value);
    }

    public string Default
    {
        get => DefaultChecked.ToString();
        set => DefaultChecked = bool.Parse(value);
    }
}
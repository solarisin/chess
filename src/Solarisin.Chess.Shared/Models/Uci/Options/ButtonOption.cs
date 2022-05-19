namespace Solarisin.Chess.Shared.Models.Uci.Options;

public class ButtonOption : IUciOption
{
    public ButtonOption(string name)
    {
        Name = name;
        Type = OptionType.Button;
    }

    public string Name { get; set; }

    public OptionType Type { get; set; }

    public string Value
    {
        get => "";
        set => throw new InvalidOperationException("Button options do not have a value");
    }

    public string Default => "";
}
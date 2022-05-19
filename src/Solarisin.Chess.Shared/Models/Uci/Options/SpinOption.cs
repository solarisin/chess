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

    public int Spin { get; set; }

    public int DefaultSpin { get; }

    public int Min { get; }

    public int Max { get; }
    public string Name { get; set; }

    public OptionType Type { get; }

    public string Value
    {
        get => Spin.ToString();
        set => Spin = int.Parse(value);
    }

    public string Default => DefaultSpin.ToString();
}
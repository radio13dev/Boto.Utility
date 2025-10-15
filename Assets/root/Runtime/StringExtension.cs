using Unity.Mathematics;

public static class StringExtension
{
    public static string ToMulString(this float value)
    {
        return "x" + (1 + value).ToString("N1");
    }
    public static string ToGemString(this long value)
    {
        return value.ToString("N0");
    }
    public static string ToGemChangeString(this long value)
    {
        return (value >= 0 ? "+" : "-") + math.abs(value).ToString("N0");
    }
    public static string ToValueChangeString(this int value)
    {
        return (value >= 0 ? "+" : "-") + math.abs(value).ToString("N0");
    }
    public static string ToValueChangeString(this long value)
    {
        return (value >= 0 ? "+" : "-") + math.abs(value).ToString("N0");
    }
}
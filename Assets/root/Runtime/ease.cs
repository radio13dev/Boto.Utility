using System;
using System.Runtime.CompilerServices;
using Unity.Mathematics;

[Obsolete("Did you mean 'ease'?")]
public static class movetowards
{
}

public static class ease
{
    public enum Mode
    {
        cubic,
        cubic_out,
        elastic_out,
        elastic_inout2,
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Evaluate(this Mode mode, float progress)
    {
        return mode switch
        {
            Mode.cubic => cubic(progress),
            Mode.cubic_out => cubic_out(progress),
            Mode.elastic_out => elastic_out(progress),
            Mode.elastic_inout2 => elastic_inout2(progress),
            _ => progress
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float cubic_inout(float progress) => cubic(progress);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float cubic(float progress)
    {
        return progress < 0.5 ? 4 * progress * progress * progress : 1 - math.pow(-2 * progress + 2, 3) / 2;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float cubic_in(float x)
    {
        return x * x * x;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float cubic_out(float x)
    {
        return 1 - math.pow(1 - x, 5);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float elastic_out(float x)
    {
        const float c4 = math.PI2 / 3;

        return x == 0 ? 0
            : x == 1 ? 1
            : math.pow(2, -10 * x) * math.sin((x * 10 - 0.75f) * c4) + 1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float elastic_inout(float x)
    {
        const float c5 = (2 * math.PI) / 4.5f;

        return x == 0 ? 0
            : x == 1 ? 1
            : x < 0.5f ? -(math.pow(2, 20 * x - 10) * math.sin((20 * x - 11.125f) * c5)) / 2
            : (math.pow(2, -20 * x + 10) * math.sin((20 * x - 11.125f) * c5)) / 2 + 1;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float elastic_inout2(float x)
    {
        const float c5 = (2 * math.PI) / 4.5f;

        return x <= 0 ? 0
            : x >= 1 ? 1
            : x < 0.5f ? 32*math.pow(x,6)
            : (math.pow(2, -20 * x + 10) * math.sin((20 * x - 11.125f) * c5)) / 2 + 1;
    }
}
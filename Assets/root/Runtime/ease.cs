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
        cubic_out
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Evaluate(this Mode mode, float progress)
    {
        return mode switch
        {
            Mode.cubic => cubic(progress),
            Mode.cubic_out => cubic_out(progress),
            _ => progress
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float cubic(float progress)
    {
        return progress < 0.5 ? 4 * progress * progress * progress : 1 - math.pow(-2 * progress + 2, 3) / 2;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float cubic_out(float x)
    {
        return 1 - math.pow(1 - x, 5);
    }
}
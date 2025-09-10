using System;
using System.Runtime.CompilerServices;

public readonly struct curve : IEquatable<curve>
{
    public enum mode
    {
        constant,
        exponential,
        linear
    }
    
    public readonly mode Mode;
    public readonly float Zero;

    private curve(mode mode, float zero)
    {
        Mode = mode;
        Zero = zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public float Evaluate(int step)
    {
        if (step <= 0) return 0;
        
        return Mode switch
        {
            mode.exponential => Zero * (1 << (step-1)),
            mode.linear => Zero * step,
            _ => 0
        };
    }

    public static readonly curve zero = default;
    public static curve constant(float zero)
    {
        return new curve(mode.constant, zero);
    }
    public static curve exponential(float zero)
    {
        return new curve(mode.exponential, zero);
    }
    public static curve linear(float zero)
    {
        return new curve(mode.linear, zero);
    }

    public bool Equals(curve other)
    {
        return Mode == other.Mode && Zero.Equals(other.Zero);
    }

    public override bool Equals(object obj)
    {
        return obj is curve other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine((int)Mode, Zero);
    }

    public static bool operator ==(curve left, curve right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(curve left, curve right)
    {
        return !left.Equals(right);
    }

    public float this[int specificLevel]
    {
        get => Evaluate(specificLevel);
    }
}
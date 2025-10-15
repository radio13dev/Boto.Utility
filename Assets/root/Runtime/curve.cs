using System;
using System.Runtime.CompilerServices;

public readonly struct curve : IEquatable<curve>
{
    public enum mode
    {
        constant,
        exponential,
        linear,
        linearFromZero,
    }
    
    public readonly mode Mode;
    public readonly float Zero;
    public readonly float Scale;

    private curve(mode mode, float zero, float scale)
    {
        Mode = mode;
        Zero = zero;
        Scale = scale;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public float Evaluate(int step)
    {
        return Mode switch
        {
            mode.constant => Zero,
            mode.exponential => Zero + step > 0 ? Scale * (1 << (step-1)) : 0,
            mode.linear => Zero + Scale * step,
            mode.linearFromZero => step > 0 ? Zero + Scale * step : 0,
            _ => 0
        };
    }

    public static readonly curve zero = default;
    public static curve constant(float zero)
    {
        return new curve(mode.constant, zero, 0);
    }
    public static curve exponential(float scale)
    {
        return new curve(mode.exponential, 0, scale);
    }
    public static curve linear(float zero, float scale)
    {
        return new curve(mode.linear, zero, scale);
    }
    public static curve linearFromZero(float zero, float scale)
    {
        return new curve(mode.linearFromZero, zero, scale);
    }

    public bool Equals(curve other)
    {
        return Mode == other.Mode && Zero.Equals(other.Zero) && Scale.Equals(other.Scale);
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
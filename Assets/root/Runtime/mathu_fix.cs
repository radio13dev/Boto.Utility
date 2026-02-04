using System;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using Deterministic.FixedPoint;
using UnityEngine;
using Unity.Mathematics.Fixed;

public static partial class mathu
{
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static fp MoveTowards(fp current, fp target, fp maxDelta)
    {
        if (target-current > maxDelta) return current + maxDelta;
        return target;
    }
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float3 MoveTowards(float3 current, float3 target, fp maxDelta)
    {
        var v = target - current;
        var sqrMagnitude = math.lengthsq(v);
        if (math.all(v == float3.zero) || (maxDelta >= fp._0 && sqrMagnitude <= maxDelta * maxDelta))
            return target;

        var magnitude = fixmath.Sqrt(sqrMagnitude);
        return current + v / magnitude * maxDelta;
    }
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float2 MoveTowards(float2 current, float2 target, fp maxDelta)
    {
        var v = target - current;
        var sqrMagnitude = math.lengthsq(v);
        if (math.all(v == float2.zero) || (maxDelta >= fp._0 && sqrMagnitude <= maxDelta * maxDelta))
            return target;

        var magnitude = fixmath.Sqrt(sqrMagnitude);
        return current + v / magnitude * maxDelta;
    }
    
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Contains(this float2 range, fp v)
    {
        return range.x < v && v <= range.y;
    }
    
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float3 perpendicular(float3 n)
    {
        n = math.normalizesafe(n);

        // Frisvad's method for building an orthonormal basis
        if (n.z < -fp._0_999) // Handle edge case
        {
            return new float3(0, -1, 0);
        }

        var a = 1 / (1 + n.z);
        var b = -n.x * n.y * a;

        // x-axis of the basis (perpendicular to n)
        float3 tangent = new float3(
            1 - n.x * n.x * a,
            b,
            -n.x
        );
        return math.normalize(tangent);
    }
    
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static fp modabs(fp x, fp m)
    {
        var r = x % m;
        if (r >= 0) return r;
        return r + m;
    }
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static fp lerpangle(fp a, fp b, fp t)
    {
        var num = repeat(b - a, fp.pi);
        if (num > fp.pi_half)
            num -= fp.pi;
        return a + num * math.clamp(t,0,1);
    }
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static fp lerprepeat(fp a, fp b, fp t, fp length)
    {
        var num = repeat(b - a, length*2);
        if (num > length)
            num -= length*2;
        return a + num * math.clamp(t,0,1);
    }
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float2 lerprepeat(float2 a, float2 b, fp t, float2 length)
    {
        return new float2(lerprepeat(a.x,b.x,t,length.x), lerprepeat(a.y,b.y,t,length.y));
    }
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static fp repeat(fp t, fp length)
    {
        return math.clamp(t - math.floor(t / length) * length, 0, length);
    }
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float2 repeat(float2 t, float2 length)
    {
        return new float2(repeat(t.x, length.x), repeat(t.y, length.y));
    }
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static fp deltaangle(fp current, fp target)
    {
      fp num = repeat(target - current, fp.pi2);
      if (num > fp.pi)
        num -= fp.pi2;
      return num;
    }
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static fp deltarepeat(fp current, fp target, fp length)
    {
      fp num = repeat(target - current, length*2);
      if (num > length)
        num -= length*2;
      return num;
    }
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float2 deltarepeat(float2 current, float2 target, float2 length)
    {
        return new float2(deltarepeat(current.x, target.x, length.x), deltarepeat(current.y, target.y, length.y));
    }
}
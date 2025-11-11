using System;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;

public static class mathu
{
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float MoveTowards(float current, float target, float maxDelta)
    {
        return math.abs((double)target - (double)current) <= maxDelta ? target : current + math.sign(target - current) * maxDelta;
    }
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float3 MoveTowards(float3 current, float3 target, float maxDelta)
    {
        return math.distancesq(target,current) <= maxDelta*maxDelta ? target : current + math.normalize(target - current) * maxDelta;
    }
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float2 MoveTowards(float2 current, float2 target, float maxDelta)
    {
        return math.distancesq(target,current) <= maxDelta*maxDelta ? target : current + math.normalize(target - current) * maxDelta;
    }
    
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Contains(this float2 range, float v)
    {
        return range.x < v && v <= range.y;
    }
    
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float3 perpendicular(float3 n)
    {
        n = math.normalizesafe(n);

        // Frisvad's method for building an orthonormal basis
        if (n.z < -0.9999999f) // Handle edge case
        {
            return new float3(0, -1, 0);
        }

        float a = 1.0f / (1.0f + n.z);
        float b = -n.x * n.y * a;

        // x-axis of the basis (perpendicular to n)
        float3 tangent = new float3(
            1.0f - n.x * n.x * a,
            b,
            -n.x
        );
        return math.normalize(tangent);
    }
    
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int modabs(int x, int m)
    {
        var r = x % m;
        if (r >= 0) return r;
        return r + m;
    }
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float modabs(float x, float m)
    {
        var r = x % m;
        if (r >= 0) return r;
        return r + m;
    }
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2Int modabs(Vector2Int x, Vector2Int m)
    {
        return new Vector2Int((int)modabs(x.x,m.x), (int)modabs(x.y,m.y));
    }
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float lerpangle(float a, float b, float t)
    {
        var num = repeat(b - a, math.PI);
        if (num > math.PIHALF)
            num -= math.PI;
        return a + num * math.clamp(t,0,1);
    }
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float lerprepeat(float a, float b, float t, float length)
    {
        var num = repeat(b - a, length*2);
        if (num > length)
            num -= length*2;
        return a + num * math.clamp(t,0,1);
    }
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float repeat(float t, float length)
    {
        return math.clamp(t - math.floor(t / length) * length, 0.0f, length);
    }
}
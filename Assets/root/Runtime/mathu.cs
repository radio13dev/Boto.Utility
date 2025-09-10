using System;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;

public static class mathu
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float MoveTowards(float current, float target, float maxDelta)
    {
        return math.abs((double)target - (double)current) <= maxDelta ? target : current + math.sign(target - current) * maxDelta;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float3 MoveTowards(float3 current, float3 target, float maxDelta)
    {
        return math.distancesq(target,current) <= maxDelta*maxDelta ? target : current + math.normalize(target - current) * maxDelta;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float2 MoveTowards(float2 current, float2 target, float maxDelta)
    {
        return math.distancesq(target,current) <= maxDelta*maxDelta ? target : current + math.normalize(target - current) * maxDelta;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Contains(this float2 range, float v)
    {
        return range.x < v && v <= range.y;
    }
    
    
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
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int modabs(int x, int m)
    {
        var r = x % m;
        if (r >= 0) return r;
        return r + m;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float modabs(float x, float m)
    {
        var r = x % m;
        if (r >= 0) return r;
        return r + m;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2Int modabs(Vector2Int x, Vector2Int m)
    {
        return new Vector2Int((int)modabs(x.x,m.x), (int)modabs(x.y,m.y));
    }
}
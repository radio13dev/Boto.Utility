using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using Unity.Mathematics.Fixed;
using UnityEngine;

public static partial class VectorUtilities
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    public static float2 RotateVector2D(float2 vector, fp radians)
    {
        var sin = math.sin(radians);
        var cos = math.cos(radians);
        var tx = vector.x;
        var ty = vector.y;
        vector.x = (cos * tx) - (sin * ty);
        vector.y = (sin * tx) + (cos * ty);
        return vector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    public static float3 Average(params float3[] vectors)
    {
        float3 sum = float3.zero;
        foreach(var v in vectors)
        {
            sum += v;
        }
        return sum / vectors.Length;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    public static float3 PointAt(float3 start, float3 target)
    {
        return math.normalize(target - start);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    public static float2 PointAt(float2 start, float2 target)
    {
        return math.normalize(target - start);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    public static float3 xy0(this float2 p) => new float3(p.x, p.y, 0);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    public static float3 TurnRight(this float3 vector)
    {
        return new float3(vector.z, vector.y, -vector.x);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    public static float3 TurnLeft(this float3 vector)
    {
        return new float3(-vector.z, vector.y, vector.x);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    public static float2 TurnRight(this float2 vector)
    {
        return new float2(vector.y, -vector.x);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    public static float2 TurnLeft(this float2 vector)
    {
        return new float2(-vector.y, vector.x);
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    public static float3 f3(this float2 vector) => new float3(vector, 0);
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    public static float3 f3(this int2 vector) => new float3(vector, 0);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    public static float3 f3z(this float2 vector) => new float3(vector.x, 0, vector.y);
}
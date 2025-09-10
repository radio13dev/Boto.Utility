using Drawing;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public static class DebugUtility
{
    public static void DrawDebugCone(float3 center, float3 direction, float3 tangent, float angle, Color color)
    {
        var len = math.length(direction);
        var directionNorm = math.normalize(direction);
        var tangentNorm = math.normalize(tangent);
        var range0 = math.normalize(math.mul(quaternion.AxisAngle(tangentNorm, angle), directionNorm));
        float3 last = range0;
        for (int i = 1; i < 21; i++)
        {
            var newLine = math.normalize(math.mul(quaternion.AxisAngle(directionNorm, math.PI2 * i / 20.0f), range0)) * len;
            Debug.DrawLine(center, center + newLine, color);
            Debug.DrawLine(center + last, center + newLine, color);
            last = newLine;
        }

        Debug.DrawLine(center, center + direction, color * new Color(1, 1, 1, 0.5f));
        Debug.DrawLine(center, center + tangent, color * new Color(1, 1, 1, 0.5f));
    }

    public static void DrawDebugBox(float3 center, float3 size, LocalToWorld t)
        => DrawDebugBox(center, t.Value.TransformDirection(size), LocalTransform.FromMatrix(t.Value));

    public static void DrawDebugBox(float3 center, float3 size, Transform t)
        => DrawDebugBox(center, new float3(size.x * t.lossyScale.x, size.y * t.lossyScale.y, size.z * t.lossyScale.z), LocalTransform.FromMatrix(t.localToWorldMatrix));

    public static void DrawDebugBox(float3 center, float3 size, LocalTransform t)
    {
        center = t.TransformPoint(center);
        //size = t.TransformDirection(size);
        float3 right = math.normalize(t.TransformDirection(new float3(1, 0, 0)));
        float3 up = math.normalize(t.TransformDirection(new float3(0, 1, 0)));
        float3 forward = math.normalize(t.TransformDirection(new float3(0, 0, 1)));

        float3 width, depth, height;
        width = right * math.dot(size, right);
        depth = forward * math.dot(size, forward);
        height = up * math.dot(size, up);

        var p00 = center - width - depth - height;
        var p01 = center + width - depth - height;
        var p10 = center - width + depth - height;
        var p11 = center + width + depth - height;

        var q00 = center - width - depth + height;
        var q01 = center + width - depth + height;
        var q10 = center - width + depth + height;
        var q11 = center + width + depth + height;

        Color color = Color.white;
        Debug.DrawLine(p00, p01, color);
        Debug.DrawLine(p01, p11, color);
        Debug.DrawLine(p11, p10, color);
        Debug.DrawLine(p10, p00, color);

        Debug.DrawLine(q00, q01, color);
        Debug.DrawLine(q01, q11, color);
        Debug.DrawLine(q11, q10, color);
        Debug.DrawLine(q10, q00, color);

        Debug.DrawLine(p00, q00, color);
        Debug.DrawLine(p01, q01, color);
        Debug.DrawLine(p10, q10, color);
        Debug.DrawLine(p11, q11, color);
    }

    public static void DrawDebugSphere(float3 center, float r, Color color)
    {
        var steps = 8;
        var angChange = math.PI2 / steps;

        var rVec = new float3(r, 0, 0);
        var axis = new float3(0, 1, 0);
        float3 lastLine = rVec;
        for (int i = 1; i < steps + 1; i++)
        {
            var newLine = math.mul(quaternion.AxisAngle(axis, angChange * i), rVec);
            Draw.Line(center + lastLine, center + newLine, color);
            lastLine = newLine;
        }

        rVec = new float3(r, 0, 0);
        axis = new float3(0, 0, 1);
        lastLine = rVec;
        for (int i = 1; i < steps + 1; i++)
        {
            var newLine = math.mul(quaternion.AxisAngle(axis, angChange * i), rVec);
            Draw.Line(center + lastLine, center + newLine, color);
            lastLine = newLine;
        }

        rVec = new float3(0, r, 0);
        axis = new float3(1, 0, 0);
        lastLine = rVec;
        for (int i = 1; i < steps + 1; i++)
        {
            var newLine = math.mul(quaternion.AxisAngle(axis, angChange * i), rVec);
            Draw.Line(center + lastLine, center + newLine, color);
            lastLine = newLine;
        }
    }
    
    public static void DrawLine(float3 start, float3 end, Color color)
    {
        
    }
}
using Unity.Mathematics;
using UnityEngine;

public static class PlaneUtility
{
    public static float RayPlaneDistance(float3 rayOrigin, float3 rayDirection, float3 planePoint, float3 planeNormal)
    {
        // Compute the dot product between ray direction and plane normal.
        float denominator = math.dot(rayDirection, planeNormal);

        // When denominator is approximately zero, the ray is parallel to the plane.
        if (Mathf.Approximately(denominator, 0))
            return 0f;
        
        // Compute the intersection distance using the plane equation.
        float distance = math.dot(planePoint - rayOrigin, planeNormal) / denominator;
        return distance;
    }
    public static float3 RayPlanePosition(float3 rayOrigin, float3 rayDirection, float3 planePoint, float3 planeNormal)
    {
        var dist = PlaneUtility.RayPlaneDistance(rayOrigin, rayDirection, planePoint, planeNormal);
        return dist*rayDirection + rayOrigin;
    }
}
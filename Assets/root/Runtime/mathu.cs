using Unity.Mathematics;

public static class mathu
{
    public static float MoveTowards(float current, float target, float maxDelta)
    {
        return math.abs((double)target - (double)current) <= maxDelta ? target : current + math.sign(target - current) * maxDelta;
    }
    public static float3 MoveTowards(float3 current, float3 target, float maxDelta)
    {
        return math.distancesq(target,current) <= maxDelta*maxDelta ? target : current + math.normalize(target - current) * maxDelta;
    }
    public static float2 MoveTowards(float2 current, float2 target, float maxDelta)
    {
        return math.distancesq(target,current) <= maxDelta*maxDelta ? target : current + math.normalize(target - current) * maxDelta;
    }
}
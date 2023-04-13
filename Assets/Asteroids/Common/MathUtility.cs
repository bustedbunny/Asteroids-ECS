using System;
using Unity.Mathematics;

namespace Asteroids
{
    // Just some math utilities copied from Unity engine
    public static class MathUtility
    {
        public static float SignedAngle(float3 from, float3 to, float3 axis)
        {
            float num1 = Angle(from, to);
            float num2 = (float)((double)from.y * (double)to.z - (double)from.z * (double)to.y);
            float num3 = (float)((double)from.z * (double)to.x - (double)from.x * (double)to.z);
            float num4 = (float)((double)from.x * (double)to.y - (double)from.y * (double)to.x);
            float num5 = math.sign((float)((double)axis.x * (double)num2 + (double)axis.y * (double)num3 +
                                           (double)axis.z * (double)num4));
            return num1 * num5;
        }

        public static float Angle(float3 from, float3 to)
        {
            float num = (float)math.sqrt((double)math.lengthsq(from) * (double)math.lengthsq(to));
            return (double)num < 1.0000000036274937E-15
                ? 0.0f
                : (float)Math.Acos((double)math.clamp(math.dot(from, to) / num, -1f, 1f)) * 57.29578f;
        }
    }
}
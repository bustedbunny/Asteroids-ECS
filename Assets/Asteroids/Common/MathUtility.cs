using System;
using Unity.Mathematics;

namespace Asteroids
{
    // Just some math utilities copied from Unity engine
    public static class MathUtility
    {
        public static float SignedAngle(float2 from, float2 to) => Angle(from, to) *
                                                                   math.sign((float)(from.x * (double)to.y -
                                                                       from.y * (double)to.x));

        public static float Angle(float2 from, float2 to)
        {
            float num = (float)Math.Sqrt(math.lengthsq(from) * (double)math.lengthsq(to));
            return num < 1.0000000036274937E-15
                ? 0.0f
                : (float)Math.Acos(math.clamp(math.dot(from, to) / num, -1f, 1f)) * 57.29578f;
        }
    }
}
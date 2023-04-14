using Unity.Mathematics;

namespace Asteroids
{
    public static class SpawnUtility
    {
        private const float SpawnOffset = 1f;

        public static float2 GetSpawnPos(float angle)
        {
            const float radius = Constants.XBounds + SpawnOffset;
            math.sincos(angle, out var sin, out var cos);
            return new float2(cos, sin) * radius;
        }
    }
}
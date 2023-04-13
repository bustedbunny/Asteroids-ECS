using Asteroids.Data.Common;
using Unity.Mathematics;

namespace Asteroids.Data
{
    public class Velocity : Comp<Velocity>
    {
        public float2 linear;
        public float angular;
    }
}
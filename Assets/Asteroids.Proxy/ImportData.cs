using System;
using Unity.Mathematics;

namespace Asteroids.Proxy
{
    [Serializable]
    public struct ImportData
    {
        public float2 movement;
        public bool shoot;
        public bool laserShoot;
    }
}
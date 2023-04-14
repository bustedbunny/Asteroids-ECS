using System;
using Unity.Mathematics;

namespace Asteroids.Runtime.DataTransfer
{
    [Serializable]
    public struct Input
    {
        public float2 movement;
        public bool shoot;
        public bool laserShoot;
    }
}
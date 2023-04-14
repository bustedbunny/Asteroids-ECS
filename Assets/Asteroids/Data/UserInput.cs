using System;
using Asteroids.ECS;
using Unity.Mathematics;

namespace Asteroids.Data
{
    public class UserInput : Comp<UserInput>
    {
        public Input value;
    }

    [Serializable]
    public struct Input
    {
        public float2 movement;
        public bool shoot;
        public bool laserShoot;
    }
}
using System;
using Asteroids.ECS;
using Unity.Mathematics;

namespace Asteroids.Data
{
    [Serializable]
    public class Transform : Comp<Transform>
    {
        public float2 position;
        public float angle;
    }
}
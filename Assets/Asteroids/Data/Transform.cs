using System;
using Asteroids.Data.Common;
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
using System;
using Asteroids.ECS;
using Unity.Mathematics;

namespace Asteroids.Data
{
    public class UserInputSingleton : Comp<UserInputSingleton>
    {
        public UserInput value;
    }

    [Serializable]
    public struct UserInput
    {
        public float2 movement;
        public bool shoot;
    }
}
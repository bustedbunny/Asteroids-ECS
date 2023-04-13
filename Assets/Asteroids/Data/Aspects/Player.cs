using Asteroids.ECS.Asteroids.ECS;

namespace Asteroids.Data.Aspects
{
    public class Player : Comp<Player>
    {
        protected override void OnCreate()
        {
            Input = Parent.TryAddComp<UserInputSingleton>();
            Velocity = Parent.TryAddComp<Velocity>();
            Transform = Parent.TryAddComp<Transform>();
        }

        public Transform Transform { get; private set; }
        public Velocity Velocity { get; private set; }
        public UserInputSingleton Input { get; private set; }
    }
}
using Asteroids.ECS.Asteroids.ECS;

namespace Asteroids.Data.Aspects
{
    public class Ufo : Comp<Ufo>
    {
        public float rotationSpeed = 300f;
        public float movementSpeed = 1f;

        public Velocity Velocity { get; private set; }
        public Transform Transform { get; private set; }

        protected override void OnCreate()
        {
            Velocity = Parent.GetComp<Velocity>();
            Transform = Parent.GetComp<Transform>();
        }
    }
}
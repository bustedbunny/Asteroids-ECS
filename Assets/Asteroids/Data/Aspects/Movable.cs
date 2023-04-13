using Asteroids.Data.Common;

namespace Asteroids.Data.Aspects
{
    public class Movable : Comp<Movable>
    {
        public Transform Transform { get; private set; }
        public Velocity Velocity { get; private set; }

        public override void OnCreate()
        {
            Transform = Parent.GetComp<Transform>();
            Velocity = Parent.GetComp<Velocity>();
        }

        public override void OnUpdate()
        {
            var delta = Time.delta;
            Transform.position += Velocity.linear * delta;
            Transform.angle += Velocity.angular * delta;
        }
    }
}
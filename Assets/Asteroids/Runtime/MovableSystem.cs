using Asteroids.Data.Aspects;
using Asteroids.Runtime.Common;

namespace Asteroids.Runtime
{
    public class MovableSystem : BaseSystem
    {
        private ComponentQuery<Movable> _query;

        protected override void OnCreate()
        {
            _query = World.QueryStore.GetStore<Movable>();
        }

        protected override void OnUpdate()
        {
            var delta = Time.delta;
            foreach (var movable in _query)
            {
                movable.Transform.position += movable.Velocity.linear * delta;
                movable.Transform.angle += movable.Velocity.angular * delta;
            }
        }
    }
}
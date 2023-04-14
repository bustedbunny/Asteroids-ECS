using Asteroids.Data.Aspects;
using Asteroids.ECS;
using Unity.Mathematics;

namespace Asteroids.Runtime
{
    public class MovableSystem : BaseSystem
    {
        private ComponentQuery<Movable> _query;

        protected override void OnCreate()
        {
            _query = World.QueryStore.GetQuery<Movable>();
        }

        protected override void OnUpdate()
        {
            var delta = Time.delta;
            foreach (var movable in _query)
            {
                var rotation = quaternion.Euler(0f, movable.Transform.angle, 0f);
                var forward = math.forward(rotation);
                forward = math.normalize(forward);


                var velocity = movable.Velocity;

                movable.Transform.position += 5f * forward.xz * velocity.forwardLinear * delta;
                movable.Transform.angle += velocity.angular * delta;
                movable.Transform.angle %= math.PI * 2f;
            }
        }
    }
}
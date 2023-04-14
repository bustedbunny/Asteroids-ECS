using System;
using Asteroids.Data.Aspects;
using Asteroids.ECS.Asteroids.ECS;
using Unity.Mathematics;

namespace Asteroids.Runtime
{
    public class UfoSystem : BaseSystem
    {
        private ComponentQuery<Ufo> _query;
        private ComponentQuery<Player> _playerQuery;

        protected override void OnCreate()
        {
            _query = World.QueryStore.GetQuery<Ufo>();
            _playerQuery = World.QueryStore.GetQuery<Player>();
            RequireForUpdate<Player>();
        }

        protected override void OnUpdate()
        {
            var delta = Time.delta;
            var playerPos = _playerQuery.GetSingleton().Transform.position;
            foreach (var ufo in _query)
            {
                var tf = ufo.Transform;
                var velocity = ufo.Velocity;

                var direction = playerPos - tf.position;
                direction = math.normalize(direction);

                var forward = math.forward(quaternion.Euler(0f, tf.angle, 0f)).xz;

                var targetAngle = MathUtility.SignedAngle(direction, forward);

                var deltaAngle = targetAngle - tf.angle;
                var sign = math.sign(deltaAngle);

                var rotationVelocity = math.clamp(math.abs(deltaAngle), 0f, ufo.rotationSpeed) * sign;
                rotationVelocity *= delta;
                velocity.angular = rotationVelocity;
                velocity.forwardLinear = ufo.movementSpeed;
            }
        }
    }
}
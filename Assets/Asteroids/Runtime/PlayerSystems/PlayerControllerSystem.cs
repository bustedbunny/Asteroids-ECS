﻿using Asteroids.Data;
using Asteroids.Data.Aspects;
using Asteroids.ECS;
using Unity.Mathematics;

namespace Asteroids.Runtime.PlayerSystems
{
    public class PlayerControllerSystem : BaseSystem
    {
        private ComponentQuery<Player> _playerQuery;

        protected override void OnCreate()
        {
            _playerQuery = World.QueryStore.GetQuery<Player>();
        }

        protected override void OnUpdate()
        {
            var delta = Time.delta;

            foreach (var player in _playerQuery)
            {
                var velocity = player.Velocity;
                var input = player.Input.value;


                // Teleport
                var curPos = player.Transform.position;
                var posSigns = math.sign(curPos);
                curPos = math.abs(curPos);
                if (curPos.x >= Constants.XBounds)
                {
                    player.Transform.position.x += -posSigns.x * Constants.XBounds * 2f;
                }

                if (curPos.y >= Constants.YBounds)
                {
                    player.Transform.position.y += -posSigns.y * Constants.YBounds * 2f;
                }


                // Input
                var deltaVelocity = input.movement * delta;
                velocity.forwardLinear += deltaVelocity.y;
                velocity.angular = deltaVelocity.x * 200f;

                // Inertia
                var falloff = 1f / 7f * delta;
                var linearSign = math.sign(velocity.forwardLinear);
                velocity.forwardLinear = math.max(0f, math.abs(velocity.forwardLinear) - falloff) * linearSign;

                var tf = player.Transform;
                // Bullet
                if (input.shoot)
                {
                    var bullet = Prefabs.Projectile(World, GraphicsDef.PlayerBullet, true);
                    bullet.Transform.position = tf.position;
                    bullet.Transform.angle = tf.angle;
                    bullet.Velocity.forwardLinear = 5f + velocity.forwardLinear;
                }

                // Laser
                if (input.laserShoot && player.Laser.currentCharges > 0)
                {
                    player.Laser.currentCharges--;
                    var laser = Prefabs.Projectile(World, GraphicsDef.PlayerLaser, false);
                    laser.Transform.position = tf.position;
                    laser.Transform.angle = tf.angle;
                    laser.Velocity.forwardLinear = 10f + velocity.forwardLinear;
                }
            }
        }
    }
}
using System.Collections.Generic;
using Asteroids.Data.Aspects;
using Asteroids.ECS.Asteroids.ECS;
using Unity.Mathematics;

namespace Asteroids.Runtime
{
    public class BulletSystem : BaseSystem
    {
        private ComponentQuery<Bullet> _bulletQuery;
        private ComponentQuery<Enemy> _enemyQuery;

        protected override void OnCreate()
        {
            _bulletQuery = World.QueryStore.GetQuery<Bullet>();
            RequireForUpdate<Bullet>();
            _enemyQuery = World.QueryStore.GetQuery<Enemy>();
        }

        private readonly List<Bullet> _bulletsToDestroy = new(1);

        protected override void OnUpdate()
        {
            var delta = Time.delta;
            foreach (var bullet in _bulletQuery)
            {
                bullet.timeLeft -= delta;
                if (bullet.timeLeft <= 0f)
                {
                    _bulletsToDestroy.Add(bullet);
                    continue;
                }

                foreach (var enemy in _enemyQuery)
                {
                    var bulletPos = bullet.Transform.position;
                    var enemyPos = enemy.Transform.position;
                    if (math.distance(bulletPos, enemyPos) <= 1f)
                    {
                        _bulletsToDestroy.Add(bullet);
                        break;
                    }
                }
            }

            foreach (var bullet in _bulletsToDestroy)
            {
                World.DestroyEntity(bullet.Parent);
            }
        }
    }
}
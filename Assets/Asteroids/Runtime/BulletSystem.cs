using System.Collections.Generic;
using Asteroids.Data.Aspects;
using Asteroids.ECS;
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

        private readonly List<Entity> _entitiesToDestroy = new(1);
        private readonly List<Enemy> _enemiesToDestroy = new(1);

        protected override void OnUpdate()
        {
            var delta = Time.delta;
            foreach (var bullet in _bulletQuery)
            {
                bullet.timeLeft -= delta;
                if (bullet.timeLeft <= 0f)
                {
                    _entitiesToDestroy.Add(bullet.Parent);
                    continue;
                }

                foreach (var enemy in _enemyQuery)
                {
                    var bulletPos = bullet.Transform.position;
                    var enemyPos = enemy.Transform.position;
                    if (math.distance(bulletPos, enemyPos) <= enemy.collisionRadius)
                    {
                        _entitiesToDestroy.Add(bullet.Parent);
                        _entitiesToDestroy.Add(enemy.Parent);
                        _enemiesToDestroy.Add(enemy);
                        break;
                    }
                }
            }

            // This must be done outside of query because asteroid spawns chunks (which modifies query)
            foreach (var enemy in _enemiesToDestroy)
            {
                enemy.actionOnDeath?.Invoke();
            }

            foreach (var entity in _entitiesToDestroy)
            {
                World.DestroyEntity(entity);
            }

            _enemiesToDestroy.Clear();
            _entitiesToDestroy.Clear();
        }
    }
}
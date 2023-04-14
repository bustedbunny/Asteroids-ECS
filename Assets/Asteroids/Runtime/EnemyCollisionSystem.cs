using Asteroids.Data.Aspects;
using Asteroids.ECS;
using Unity.Mathematics;

namespace Asteroids.Runtime
{
    public class EnemyCollisionSystem : BaseSystem
    {
        private ComponentQuery<Enemy> _enemyQuery;
        private ComponentQuery<Player> _playerQuery;

        protected override void OnCreate()
        {
            _enemyQuery = World.QueryStore.GetQuery<Enemy>();
            _playerQuery = World.QueryStore.GetQuery<Player>();
            RequireForUpdate<Player>();
        }

        protected override void OnUpdate()
        {
            var playerPos = _playerQuery.GetSingleton().Transform.position;

            foreach (var enemy in _enemyQuery)
            {
                var enemyPos = enemy.Transform.position;
                if (math.distancesq(playerPos, enemyPos) <= 1f)
                {
                    World.DestroyAllEntities();
                    return;
                }
            }
        }
    }
}
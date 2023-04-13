using System.Collections.Generic;
using Asteroids.Data.Aspects;
using Asteroids.ECS.Asteroids.ECS;
using Unity.Mathematics;

namespace Asteroids.Runtime
{
    public class EnemyManagementSystem : BaseSystem
    {
        private ComponentQuery<Enemy> _enemyQuery;

        protected override void OnCreate()
        {
            _enemyQuery = World.QueryStore.GetQuery<Enemy>();
            RequireForUpdate<Player>();
        }

        private Random _random;

        private const int MaxEnemies = 10;

        private readonly List<Entity> _entitiesToDestroy = new(1);


        // Magic value explanation
        // Spawn distance = 16+1 squared = 289
        // Destroy check will be 16*16+100 = 356
        // This is to ensure enemies are not destroyed on spawn
        private const float SpawnOffset = 1f;
        private const float DestroyPosOffset = 100f;


        protected override void OnUpdate()
        {
            // Destroy asteroids if they are too far
            foreach (var enemy in _enemyQuery)
            {
                var distanceToCenterSq = math.distancesq(0f, enemy.Transform.position);
                if (distanceToCenterSq > Constants.XBounds * Constants.XBounds + DestroyPosOffset)
                {
                    _entitiesToDestroy.Add(enemy.Parent);
                }
            }

            foreach (var entity in _entitiesToDestroy)
            {
                World.DestroyEntity(entity);
            }

            _entitiesToDestroy.Clear();

            // Spawn new enemies
            if (_enemyQuery.Count >= MaxEnemies)
            {
                return;
            }

            _random = new((uint)Version);

            var enemyType = _random.NextInt(2);

            // Angle of imaginary circle to determines random spawn position
            var angle = _random.NextFloat(math.PI * 2f);

            const float radius = Constants.XBounds + SpawnOffset;
            math.sincos(angle, out var sin, out var cos);

            var pos = new float2(cos, sin) * radius;
            var randomLookPosition = _random.NextFloat2(MinBounds, MaxBounds);
            var rotationAngle = MathUtility.SignedAngle(new(pos.x, 0f, pos.y),
                new(randomLookPosition.x, 0f, randomLookPosition.y), math.up());


            var asteroid = Prefabs.Asteroid(World);
            asteroid.Transform.position = pos;
            asteroid.Transform.angle = rotationAngle;
            asteroid.Velocity.forwardLinear = 1f;
        }

        private static readonly float2 MinBounds = new float2(Constants.XBounds, Constants.YBounds) * -0.5f;
        private static readonly float2 MaxBounds = new float2(Constants.XBounds, Constants.YBounds) * 0.5f;
    }
}
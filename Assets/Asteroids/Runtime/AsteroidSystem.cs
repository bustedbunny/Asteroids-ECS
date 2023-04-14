using System.Collections.Generic;
using Asteroids.Data.Aspects;
using Asteroids.ECS;
using Unity.Mathematics;

namespace Asteroids.Runtime
{
    public class AsteroidSystem : BaseSystem
    {
        private ComponentQuery<Asteroid> _query;

        protected override void OnCreate()
        {
            _query = World.QueryStore.GetQuery<Asteroid>();
            RequireForUpdate<Player>();

            _random = new((uint)GetHashCode());
        }

        private const int MaxAsteroids = 7;

        // Magic value explanation
        // Spawn distance = 16+1 squared = 289
        // Destroy check will be 16*16+100 = 356
        // This is to ensure enemies are not destroyed on spawn
        private const float DestroyPosOffset = 100f;

        private readonly List<Entity> _entitiesToDestroy = new(1);
        private Random _random;


        protected override void OnUpdate()
        {
            // Destroy asteroids if they are too far
            foreach (var enemy in _query)
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

            if (_query.Count > MaxAsteroids)
            {
                return;
            }


            var pos = SpawnUtility.GetSpawnPos(_random.NextFloat(math.PI * 2f));
            var randomLookPosition = _random.NextFloat2(MinBounds, MaxBounds);
            var lookDirection = pos - randomLookPosition;
            lookDirection = math.normalize(lookDirection);
            var rotationAngle = MathUtility.SignedAngle(math.up().xy, lookDirection);


            var asteroid = Prefabs.Asteroid(World);
            asteroid.Transform.position = pos;
            asteroid.Transform.angle = rotationAngle;
            asteroid.Velocity.forwardLinear = 1f;
        }

        // Simulation space bounds
        private static readonly float2 MinBounds = new float2(Constants.XBounds, Constants.YBounds) * -0.5f;
        private static readonly float2 MaxBounds = new float2(Constants.XBounds, Constants.YBounds) * 0.5f;
    }
}
using Asteroids.Data.Aspects;
using Asteroids.ECS;
using Unity.Mathematics;

namespace Asteroids.Runtime
{
    public class UfoSpawnSystem : BaseSystem
    {
        private ComponentQuery<Ufo> _enemyQuery;

        protected override void OnCreate()
        {
            _enemyQuery = World.QueryStore.GetQuery<Ufo>();
            RequireForUpdate<Player>();
        }

        private Random _random;

        private const int MaxUfo = 2;


        protected override void OnUpdate()
        {
            if (_enemyQuery.Count >= MaxUfo)
            {
                return;
            }

            _random = new((uint)Version);


            var pos = SpawnUtility.GetSpawnPos(_random.NextFloat(math.PI * 2f));

            var direction = float2.zero - pos;
            direction = math.normalize(direction);

            // Rotation angle so that freshly spawned entity looks directly in the center of arena
            var lookAngle = MathUtility.SignedAngle(math.up().xy, direction);

            var ufo = Prefabs.Ufo(World);
            ufo.Transform.position = pos;
            ufo.Transform.angle = lookAngle;
            ufo.Velocity.forwardLinear = 1f;
        }
    }
}
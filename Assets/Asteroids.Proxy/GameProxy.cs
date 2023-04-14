using Asteroids.Data;
using Asteroids.ECS;
using Asteroids.Runtime;
using UnityEngine;

namespace Asteroids.Proxy
{
    public class GameProxy
    {
        private readonly GameWorld _world;
        private double _elapsed;
        private readonly ExportStreamingData _exportSystem;
        private readonly ImportInputData _importSystem;

        public GameProxy()
        {
            _world = new();
            _world.Systems.AddSystem<PlayerControllerSystem>();
            _world.Systems.AddSystem<MovableSystem>();
            _world.Systems.AddSystem<BulletSystem>();
            _world.Systems.AddSystem<EnemyCollisionSystem>();
            _world.Systems.AddSystem<UfoSpawnSystem>();
            _world.Systems.AddSystem<AsteroidSystem>();
            _world.Systems.AddSystem<UfoSystem>();
            _exportSystem = _world.Systems.AddSystem<ExportStreamingData>();
            _importSystem = _world.Systems.AddSystem<ImportInputData>();
        }

        public void Spawn()
        {
            Prefabs.Player(_world);
        }

        public ImportData Import { get; set; }
        public ExportData Export { get; private set; }

        public void Update()
        {
            const float deltaTime = 1 / 60f;
            _elapsed += deltaTime;
            _world.Update(new(deltaTime, (uint)(1000 * _elapsed)));


            // Simulate data transfer
            var json = JsonUtility.ToJson(_exportSystem.ExportData);
            Export = JsonUtility.FromJson<ExportData>(json);

            var inputJson = JsonUtility.ToJson(Import);
            _importSystem.ImportData = JsonUtility.FromJson<UserInput>(inputJson);
        }
    }
}
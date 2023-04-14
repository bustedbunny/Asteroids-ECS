using Asteroids.ECS;
using Asteroids.Runtime;
using Asteroids.Runtime.DataTransfer;
using Asteroids.Runtime.PlayerSystems;
using UnityEngine;
using Input = Asteroids.Runtime.DataTransfer.Input;

namespace Asteroids.Proxy
{
    public class GameProxy
    {
        private readonly GameWorld _world;
        private double _elapsed;
        private readonly ExportStreamingDataSystem _exportSystem;
        private readonly ImportInputDataSystem _importSystem;

        public GameProxy()
        {
            _world = new();
            _world.Systems.AddSystem<PlayerControllerSystem>();
            _world.Systems.AddSystem<MovableSystem>();
            _world.Systems.AddSystem<ProjectileSystem>();
            _world.Systems.AddSystem<EnemyCollisionSystem>();
            _world.Systems.AddSystem<UfoSpawnSystem>();
            _world.Systems.AddSystem<AsteroidSystem>();
            _world.Systems.AddSystem<LaserSystem>();
            _world.Systems.AddSystem<UfoSystem>();
            _exportSystem = _world.Systems.AddSystem<ExportStreamingDataSystem>();
            _importSystem = _world.Systems.AddSystem<ImportInputDataSystem>();
        }

        public void Spawn()
        {
            Prefabs.Player(_world);
        }

        public ImportData import;
        public ExportData Export { get; private set; }

        public void Update()
        {
            const float deltaTime = 1 / 60f;
            _elapsed += deltaTime;
            _world.Update(new(deltaTime, (uint)(1000 * _elapsed)));


            // Simulate data transfer
            var json = JsonUtility.ToJson(_exportSystem.ExportData);
            Export = JsonUtility.FromJson<ExportData>(json);

            var inputJson = JsonUtility.ToJson(import);
            _importSystem.ImportData = JsonUtility.FromJson<Input>(inputJson);
        }
    }
}
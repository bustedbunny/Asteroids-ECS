using System;
using System.Collections.Generic;
using Asteroids.Data;
using Asteroids.Data.Aspects;
using Asteroids.Data.Common;
using Asteroids.Runtime;
using Unity.Mathematics;
using UnityEngine;
using Transform = Asteroids.Data.Transform;

namespace Asteroids.Proxy
{
    public class GameProxy
    {
        private readonly GameWorld _world;

        public GameProxy()
        {
            _world = new GameWorld();
            _exportSystem = _world.Systems.AddSystem<ExportStreamingData>();
        }

        public void Spawn()
        {
            var test = new Entity(_world);
            test.AddComp<Transform>();
            test.AddComp<Exportable>();
        }

        private double _elapsed;
        private readonly ExportStreamingData _exportSystem;

        public ExportData Data { get; private set; }

        public void Update()
        {
            const float deltaTime = 1 / 60f;
            _elapsed += deltaTime;
            _world.Update(new(deltaTime, (uint)(1000 * _elapsed)));


            // Simulate data transfer
            var json = JsonUtility.ToJson(_exportSystem.ExportData);
            Data = JsonUtility.FromJson<ExportData>(json);
        }
    }

    [Serializable]
    public class ExportData
    {
        public uint structuralVersion;
        public List<Archetype> data;
    }

    [Serializable]
    public struct Archetype
    {
        public GraphicsDef def;
        public List<TransformData> transforms;
    }

    [Serializable]
    public struct TransformData
    {
        public float2 position;
        public float angle;
    }
}
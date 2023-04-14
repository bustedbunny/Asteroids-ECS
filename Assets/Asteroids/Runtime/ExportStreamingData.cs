using System;
using System.Collections.Generic;
using Asteroids.Data;
using Asteroids.Data.Aspects;
using Asteroids.ECS;
using Unity.Mathematics;

namespace Asteroids.Runtime
{
    public class ExportStreamingData : BaseSystem
    {
        private ComponentQuery<Exportable> _query;
        private ComponentQuery<Player> _playerQuery;

        protected override void OnCreate()
        {
            _playerQuery = World.QueryStore.GetQuery<Player>();
            _query = World.QueryStore.GetQuery<Exportable>();
            ExportData = new()
            {
                data = new(3)
            };
        }

        private double _lastBakeVersion;


        protected override void OnUpdate()
        {
            if (_query.OrderChanged(_lastBakeVersion))
            {
                _lastBakeVersion = Version;
                BakeExportData();
            }

            if (_playerQuery.IsEmpty)
            {
                ExportData.gameOver = true;
            }
            else
            {
                var player = _playerQuery.GetSingleton();
                ExportData.hudData = new()
                {
                    score = player.totalScore,
                    coordinates = player.Transform.position,
                    rotation = player.Transform.angle,
                    velocity = player.Velocity.forwardLinear
                };
                ExportData.gameOver = false;
            }
        }

        public ExportData ExportData { get; private set; }

        private void BakeExportData()
        {
            ExportData.structuralVersion = Version;
            ExportData.data.Clear();
            foreach (var exportable in _query)
            {
                AddExportable(exportable);
            }
        }

        private void AddExportable(Exportable exportable)
        {
            foreach (var archetype in ExportData.data)
            {
                if (archetype.def == exportable.Def)
                {
                    archetype.transforms.Add(exportable.Transform);
                    return;
                }
            }

            ExportData.data.Add(new()
            {
                def = exportable.Def,
                transforms = new() { exportable.Transform }
            });
        }
    }

    [Serializable]
    public class ExportData
    {
        public bool gameOver;
        public HudData hudData;
        public double structuralVersion;
        public List<Archetype> data;

        [Serializable]
        public struct Archetype
        {
            public GraphicsDef def;
            public List<Transform> transforms;
        }

        [Serializable]
        public struct HudData
        {
            public int score;
            public float2 coordinates;
            public float rotation;
            public float velocity;
        }
    }
}
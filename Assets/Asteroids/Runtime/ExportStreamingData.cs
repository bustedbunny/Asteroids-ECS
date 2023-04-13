using System;
using System.Collections.Generic;
using Asteroids.Data;
using Asteroids.Data.Aspects;
using Asteroids.Runtime.Common;

namespace Asteroids.Runtime
{
    public class ExportStreamingData : BaseSystem
    {
        private ComponentQuery<Exportable> _query;

        protected override void OnCreate()
        {
            _query = World.QueryStore.GetStore<Exportable>();
            ExportData = new()
            {
                data = new(3)
            };
        }

        private double _lastBakeVersion;

        protected override void OnUpdate()
        {
            if (_query.DidChange(_lastBakeVersion))
            {
                _lastBakeVersion = Version;
                BakeExportData();
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
        public double structuralVersion;
        public List<Archetype> data;
    }

    [Serializable]
    public struct Archetype
    {
        public GraphicsDef def;
        public List<Transform> transforms;
    }
}
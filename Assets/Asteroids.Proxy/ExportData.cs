using System;
using System.Collections.Generic;
using Asteroids.Data;
using Unity.Mathematics;

namespace Asteroids.Proxy
{
    [Serializable]
    public class ExportData
    {
        public uint structuralVersion;
        public List<Archetype> data;

        [Serializable]
        public struct Archetype
        {
            public GraphicsDef def;
            public List<TransformData> transforms;

            [Serializable]
            public struct TransformData
            {
                public float2 position;
                public float angle;
            }
        }
    }
}
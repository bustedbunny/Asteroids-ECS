using System;
using System.Collections.Generic;
using Asteroids.Data;
using Unity.Mathematics;

namespace Asteroids.Proxy
{
    [Serializable]
    public struct ExportData
    {
        public bool gameOver;
        public HudData hudData;
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

        [Serializable]
        public struct HudData
        {
            public float2 coordinates;
            public float rotation;
            public float velocity;
        }
    }
}
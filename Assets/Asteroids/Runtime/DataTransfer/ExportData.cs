using System;
using System.Collections.Generic;
using Asteroids.Data;
using Unity.Mathematics;

namespace Asteroids.Runtime.DataTransfer
{
    [Serializable]
    public class ExportData
    {
        public bool gameOver;
        public HudData hudData;
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
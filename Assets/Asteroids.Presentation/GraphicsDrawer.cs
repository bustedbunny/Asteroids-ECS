using System;
using System.Collections.Generic;
using Asteroids.Proxy;
using UnityEngine;

namespace Asteroids.Presentation
{
    public class GraphicsDrawer : MonoBehaviour
    {
        [SerializeField] private DrawData[] _drawData;

        public void Draw(List<ExportData.Archetype> data)
        {
            if (data is null)
            {
                return;
            }

            foreach (var archetype in data)
            {
                var drawData = GetDrawData(archetype.def);
                foreach (var tf in archetype.transforms)
                {
                    var pos = new Vector3(tf.position.x, 0f, tf.position.y);
                    var matrix = Matrix4x4.TRS(pos, Quaternion.Euler(90f, Mathf.Rad2Deg * tf.angle, 0f), Vector3.one);
                    Graphics.DrawMesh(drawData.mesh, matrix, drawData.material, 0);
                }
            }
        }

        private DrawData GetDrawData(int def)
        {
            foreach (var drawData in _drawData)
            {
                if (drawData.def == def)
                {
                    return drawData;
                }
            }

            throw new InvalidOperationException($"Draw data with def {def} is not found");
        }
    }

    [Serializable]
    public struct DrawData
    {
        public int def;
        public Mesh mesh;
        public Material material;
    }
}
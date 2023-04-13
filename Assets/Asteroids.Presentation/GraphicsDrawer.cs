using System.Collections.Generic;
using Asteroids.Proxy;
using UnityEngine;

namespace Asteroids.Presentation
{
    public class GraphicsDrawer : MonoBehaviour
    {
        [SerializeField] private Mesh _cube;
        [SerializeField] private Material _material;

        public void Draw(List<ExportData.Archetype> data)
        {
            foreach (var archetype in data)
            {
                foreach (var tf in archetype.transforms)
                {
                    var pos = new Vector3(tf.position.x, 0f, tf.position.y);
                    var matrix = Matrix4x4.TRS(pos, Quaternion.Euler(0f, Mathf.Rad2Deg * tf.angle, 0f), Vector3.one);
                    Graphics.DrawMesh(_cube, matrix, _material, 0);
                }
            }
        }
    }
}
using System;
using Asteroids.Proxy;
using UnityEngine;

namespace Asteroids.Presentation
{
    public class GameRunner : MonoBehaviour
    {
        private GameProxy _proxy;


        [SerializeField] private Mesh _cube;
        [SerializeField] private Material _material;

        private void Awake()
        {
            _proxy = new GameProxy();
            _proxy.Update();
            _proxy.Spawn();
        }

        private void FixedUpdate()
        {
            _proxy.Update();
        }

        private void Update()
        {
            foreach (var data in _proxy.Data.data)
            {
                foreach (var tf in data.transforms)
                {
                    var pos = new Vector3(tf.position.x, 0f, tf.position.y);
                    var matrix = Matrix4x4.TRS(pos, Quaternion.Euler(0f, tf.angle, 0f), Vector3.one);
                    Graphics.DrawMesh(_cube, matrix, _material, 0);
                }
            }
        }
    }
}
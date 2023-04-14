using System;
using System.Collections;
using System.Collections.Generic;

namespace Asteroids.ECS
{
    public class Systems : IEnumerable<BaseSystem>
    {
        private readonly GameWorld _world;

        public Systems(GameWorld world)
        {
            _world = world;
        }

        private List<BaseSystem> _systems = new();

        public T AddSystem<T>() where T : BaseSystem, new()
        {
            var system = new T();
            _systems.Add(system);
            system.Init(_world);
            return system;
        }

        public T GetSystem<T>() where T : BaseSystem, new()
        {
            foreach (var baseSystem in _systems)
            {
                if (baseSystem is T target)
                {
                    return target;
                }
            }

            throw new InvalidOperationException($"No system {nameof(T)} found");
        }

        public IEnumerator<BaseSystem> GetEnumerator() => _systems.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
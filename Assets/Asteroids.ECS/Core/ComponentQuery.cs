using System;
using System.Collections;
using System.Collections.Generic;

namespace Asteroids.ECS
{
    public class ComponentQuery<T> : ComponentQuery, IEnumerable<T> where T : Comp
    {
        internal override Type Type => typeof(T);
        public override bool IsEmpty => _store.Count == 0;
        public int Count => _store.Count;

        private readonly List<Comp<T>> _store = new();
        private double _version;

        public void Register(Comp<T> comp)
        {
            _store.Add(comp);
            _version = World.Version;
        }

        public void Unregister(Comp<T> comp)
        {
            _store.Remove(comp);
            _version = World.Version;
        }

        public bool OrderChanged(double version) => _version > version;

        public T GetSingleton() => _store[0] as T;

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var comp in _store)
            {
                yield return comp as T;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public abstract class ComponentQuery
    {
        protected GameWorld World { get; private set; }

        public void Init(GameWorld world)
        {
            World = world;
        }

        internal abstract Type Type { get; }

        public abstract bool IsEmpty { get; }
    }
}
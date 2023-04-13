using System;
using System.Collections;
using System.Collections.Generic;
using Asteroids.Data.Common;

namespace Asteroids
{
    public class ComponentQuery<T> : ComponentQuery, IEnumerable<T> where T : Comp
    {
        internal override Type Type => typeof(T);

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

        public bool DidChange(double version) => _version > version;

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
    }
}
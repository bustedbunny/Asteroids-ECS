using System;
using System.Collections.Generic;

namespace Asteroids.Data.Common
{
    public sealed class Entity
    {
        private List<Comp> _comps = new();

        public Entity(GameWorld world)
        {
            World = world;
        }

        public T AddComp<T>() where T : Comp, new()
        {
            #if DEBUG
            foreach (var comp in _comps)
            {
                if (comp is T target)
                {
                    throw new InvalidOperationException($"Comp of type {nameof(T)} already exists on Entity");
                }
            }

            #endif

            var newComp = new T();
            _comps.Add(newComp);
            newComp.Init(this);
            return newComp;
        }

        public T GetComp<T>()
        {
            foreach (var comp in _comps)
            {
                if (comp is T target)
                {
                    return target;
                }
            }

            throw new InvalidOperationException($"No comp {nameof(T)} found");
        }

        public GameWorld World { get; }
    }
}
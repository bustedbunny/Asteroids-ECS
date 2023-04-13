using System;
using System.Collections.Generic;

namespace Asteroids.ECS.Asteroids.ECS
{
    public sealed class Entity
    {
        private List<Comp> _comps = new();

        internal Entity(GameWorld world)
        {
            World = world;
        }

        internal void Destroy()
        {
            foreach (var comp in _comps)
            {
                comp.Destroy();
            }
        }

        public T TryAddComp<T>() where T : Comp, new()
        {
            foreach (var comp in _comps)
            {
                if (comp is T target)
                {
                    return target;
                }
            }

            return AddComp<T>();
        }

        public T AddComp<T>() where T : Comp, new()
        {
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
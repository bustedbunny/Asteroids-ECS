using System;
using System.Collections.Generic;

namespace Asteroids.ECS
{
    public abstract class BaseSystem
    {
        public void Init(GameWorld world)
        {
            World = world;
            OnCreate();
        }

        private List<ComponentQuery> _queriesForUpdate;

        protected void RequireForUpdate<T>() where T : Comp<T>
        {
            _queriesForUpdate ??= new();
            _queriesForUpdate.Add(World.QueryStore.GetQuery<T>());
        }

        protected WorldTime Time => World.Time;
        protected GameWorld World { get; private set; }
        protected virtual void OnCreate() { }
        protected abstract void OnUpdate();


        protected double PreviousVersion { get; private set; }
        protected double Version { get; private set; }

        public void Update()
        {
            if (_queriesForUpdate is not null)
            {
                foreach (var query in _queriesForUpdate)
                {
                    if (query.IsEmpty)
                    {
                        return;
                    }
                }
            }

            Version = World.Version;
            try
            {
                OnUpdate();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            PreviousVersion = Version;
        }
    }
}
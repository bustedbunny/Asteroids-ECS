using System;

namespace Asteroids.Runtime.Common
{
    public abstract class BaseSystem
    {
        public void Init(GameWorld world)
        {
            World = world;
            OnCreate();
        }

        protected WorldTime Time => World.Time;
        protected GameWorld World { get; private set; }
        protected virtual void OnCreate() { }
        protected abstract void OnUpdate();


        protected double PreviousVersion { get; private set; }
        protected double Version { get; private set; }

        public void Update()
        {
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
namespace Asteroids.ECS.Asteroids.ECS
{
    public abstract class Comp<T> : Comp where T : Comp
    {
        private ComponentQuery<T> _query;

        internal override void Init(Entity e)
        {
            base.Init(e);
            _query = Parent.World.QueryStore.GetQuery<T>();
            _query.Register(this);
        }

        internal override void Destroy()
        {
            base.Destroy();
            _query.Unregister(this);
        }
    }

    public abstract class Comp
    {
        public Entity Parent { get; private set; }

        protected WorldTime Time => Parent.World.Time;

        internal virtual void Init(Entity e)
        {
            Parent = e;
            OnCreate();
        }

        internal virtual void Destroy()
        {
            Cleanup();
        }

        protected virtual void Cleanup() { }

        protected virtual void OnCreate() { }
    }
}
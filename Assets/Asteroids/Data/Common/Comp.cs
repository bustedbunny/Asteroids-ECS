namespace Asteroids.Data.Common
{
    public abstract class Comp<T> : Comp where T : Comp
    {
        private ComponentQuery<T> _query;

        public override void Init(Entity e)
        {
            base.Init(e);
            _query = Parent.World.QueryStore.GetStore<T>();
            _query.Register(this);
        }

        public override void Cleanup()
        {
            _query.Unregister(this);
        }
    }

    public abstract class Comp
    {
        public Entity Parent { get; private set; }

        protected WorldTime Time => Parent.World.Time;

        public virtual void Init(Entity e) => Parent = e;

        public abstract void Cleanup();

        public virtual void OnCreate() { }

        public virtual void OnUpdate() { }
    }
}
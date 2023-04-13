namespace Asteroids.ECS.Asteroids.ECS
{
    public class GameWorld
    {
        public GameWorld()
        {
            QueryStore = new(this);
            Systems = new(this);
        }

        public QueryStore QueryStore { get; }
        public Systems Systems { get; }

        public WorldTime Time { get; set; }

        public double Version { get; private set; }

        public void Update(WorldTime time)
        {
            Time = time;
            Version = time.elapsed;

            foreach (var system in Systems)
            {
                system.Update();
            }
        }

        public Entity CreateEntity() => new(this);

        public void DestroyEntity(Entity entity) => entity.Destroy();
    }
}
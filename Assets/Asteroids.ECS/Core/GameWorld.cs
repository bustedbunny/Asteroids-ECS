using System.Collections.Generic;

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

        private List<Entity> _entities = new();

        public Entity CreateEntity()
        {
            var entity = new Entity(this);
            _entities.Add(entity);
            return entity;
        }

        public void DestroyEntity(Entity entity)
        {
            _entities.Remove(entity);
            entity.Destroy();
        }

        public void DestroyAllEntities()
        {
            foreach (var entity in _entities)
            {
                entity.Destroy();
            }

            _entities.Clear();
        }
    }
}
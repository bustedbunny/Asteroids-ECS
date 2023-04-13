using System.Collections.Generic;

namespace Asteroids.ECS.Asteroids.ECS
{
    public class QueryStore
    {
        private readonly GameWorld _world;

        public QueryStore(GameWorld world)
        {
            _world = world;
        }

        private readonly List<ComponentQuery> _stores = new();

        public ComponentQuery<T> GetQuery<T>() where T : Comp
        {
            foreach (var store in _stores)
            {
                if (store.Type == typeof(T))
                {
                    return (ComponentQuery<T>)store;
                }
            }

            var newStore = new ComponentQuery<T>();
            _stores.Add(newStore);
            newStore.Init(_world);
            return newStore;
        }
    }
}
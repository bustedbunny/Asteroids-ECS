using Asteroids.Data;
using Asteroids.Data.Aspects;
using Asteroids.ECS.Asteroids.ECS;

namespace Asteroids
{
    public static class Prefabs
    {
        public static Entity Player(GameWorld world)
        {
            var entity = world.CreateEntity();
            var exportable = entity.AddComp<Exportable>();
            exportable.Def = GraphicsDef.Player;
            entity.AddComp<UserInputSingleton>();
            entity.AddComp<Movable>();
            entity.AddComp<Player>();
            return entity;
        }

        public static Movable Asteroid(GameWorld world)
        {
            var entity = world.CreateEntity();
            var exportable = entity.AddComp<Exportable>();
            exportable.Def = GraphicsDef.Asteroid;
            var result = entity.AddComp<Movable>();
            entity.AddComp<Enemy>();
            return result;
        }

        public static Bullet Bullet(GameWorld world)
        {
            var entity = world.CreateEntity();
            var exportable = entity.AddComp<Exportable>();
            exportable.Def = GraphicsDef.PlayerBullet;
            entity.AddComp<Movable>();
            return entity.AddComp<Bullet>();
        }
    }
}
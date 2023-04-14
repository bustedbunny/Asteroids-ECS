using Asteroids.Data;
using Asteroids.Data.Aspects;
using Asteroids.ECS;
using Unity.Mathematics;

namespace Asteroids
{
    public static class Prefabs
    {
        public static Entity Player(GameWorld world)
        {
            var entity = world.CreateEntity();
            entity.AddComp<Transform>();
            entity.AddComp<Velocity>();

            var laser = entity.AddComp<Laser>();
            laser.maxCharges = 3;
            laser.rechargeTime = 1.5f;

            var exportable = entity.AddComp<Exportable>();
            exportable.Def = GraphicsDef.Player;
            entity.AddComp<UserInput>();
            entity.AddComp<Movable>();
            entity.AddComp<Player>();
            return entity;
        }

        public static Movable Asteroid(GameWorld world)
        {
            var entity = world.CreateEntity();
            entity.AddComp<Transform>();
            entity.AddComp<Velocity>();
            var exportable = entity.AddComp<Exportable>();
            exportable.Def = GraphicsDef.Asteroid;
            var result = entity.AddComp<Movable>();
            var enemy = entity.AddComp<Enemy>();
            enemy.collisionRadius = 1f;
            enemy.scorePoints = 2;
            enemy.actionOnDeath = () =>
            {
                const float rotationOffset = math.PI * 2f / 3f;
                for (int i = 0; i < 3; i++)
                {
                    var chunk = AsteroidChunk(world);
                    chunk.Transform.position = result.Transform.position;
                    chunk.Transform.angle = result.Transform.angle + rotationOffset * i;
                    chunk.Velocity.forwardLinear = result.Velocity.forwardLinear * 1.5f;
                }
            };
            entity.AddComp<Asteroid>();
            return result;
        }

        public static Movable AsteroidChunk(GameWorld world)
        {
            var entity = world.CreateEntity();
            entity.AddComp<Transform>();
            entity.AddComp<Velocity>();
            var exportable = entity.AddComp<Exportable>();
            exportable.Def = GraphicsDef.AsteroidChunk;
            var result = entity.AddComp<Movable>();
            var enemy = entity.AddComp<Enemy>();
            enemy.scorePoints = 1;
            enemy.collisionRadius = 0.5f;
            entity.AddComp<Asteroid>();
            return result;
        }

        public static Projectile Projectile(GameWorld world, GraphicsDef def, bool destroyOnImpact)
        {
            var entity = world.CreateEntity();
            entity.AddComp<Transform>();
            entity.AddComp<Velocity>();
            var exportable = entity.AddComp<Exportable>();
            exportable.Def = def;
            entity.AddComp<Movable>();
            var bullet = entity.AddComp<Projectile>();
            bullet.destroyOnImpact = destroyOnImpact;
            return bullet;
        }


        public static Ufo Ufo(GameWorld world)
        {
            var entity = world.CreateEntity();
            entity.AddComp<Transform>();
            entity.AddComp<Velocity>();
            var exportable = entity.AddComp<Exportable>();
            exportable.Def = GraphicsDef.Ufo;
            entity.AddComp<Movable>();
            var enemy = entity.AddComp<Enemy>();
            enemy.scorePoints = 3;
            enemy.collisionRadius = 1f;
            return entity.AddComp<Ufo>();
        }
    }
}
using Asteroids.ECS.Asteroids.ECS;

namespace Asteroids.Data.Aspects
{
    public class Asteroid : Comp<Asteroid>
    {
        public Transform Transform { get; private set; }

        protected override void OnCreate()
        {
            Transform = Parent.GetComp<Transform>();
        }
    }
}
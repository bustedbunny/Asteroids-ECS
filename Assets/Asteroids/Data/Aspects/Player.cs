using Asteroids.ECS;

namespace Asteroids.Data.Aspects
{
    public class Player : Comp<Player>
    {
        public int totalScore;

        protected override void OnCreate()
        {
            Input = Parent.GetComp<UserInput>();
            Velocity = Parent.GetComp<Velocity>();
            Transform = Parent.GetComp<Transform>();
            Laser = Parent.GetComp<Laser>();
        }

        public Transform Transform { get; private set; }
        public Velocity Velocity { get; private set; }
        public UserInput Input { get; private set; }
        public Laser Laser { get; private set; }
    }
}
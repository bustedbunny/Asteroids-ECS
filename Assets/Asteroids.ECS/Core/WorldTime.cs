namespace Asteroids.ECS
{
    public readonly struct WorldTime
    {
        public readonly float delta;
        public readonly double elapsed;

        public WorldTime(float delta, uint elapsed)
        {
            this.delta = delta;
            this.elapsed = elapsed;
        }
    }
}
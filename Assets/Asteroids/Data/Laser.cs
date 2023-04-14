using Asteroids.ECS;

namespace Asteroids.Data
{
    public class Laser : Comp<Laser>
    {
        public int maxCharges;
        public int currentCharges;
        public float rechargeTime;
        public float rechargeState;
    }
}
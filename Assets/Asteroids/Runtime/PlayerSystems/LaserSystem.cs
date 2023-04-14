using Asteroids.Data;
using Asteroids.ECS;
using Unity.Mathematics;

namespace Asteroids.Runtime.PlayerSystems
{
    public class LaserSystem : BaseSystem
    {
        private ComponentQuery<Laser> _query;

        protected override void OnCreate()
        {
            _query = World.QueryStore.GetQuery<Laser>();
        }

        protected override void OnUpdate()
        {
            var delta = Time.delta;
            foreach (var laser in _query)
            {
                if (laser.currentCharges < laser.maxCharges)
                {
                    laser.rechargeState += delta;
                    if (laser.rechargeState >= laser.rechargeTime)
                    {
                        laser.currentCharges++;
                        var isFull = laser.currentCharges == laser.maxCharges;
                        laser.rechargeState = math.select(laser.rechargeState - laser.rechargeTime, 0f, isFull);
                    }
                }
            }
        }
    }
}
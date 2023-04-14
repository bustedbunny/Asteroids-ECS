using System;
using Asteroids.ECS;

namespace Asteroids.Data.Aspects
{
    public class Enemy : Comp<Enemy>
    {
        public int scorePoints;

        // this is very anti-pattern but rather desperate measures due to lack of mature ECS solution 
        public Action actionOnDeath;

        public float collisionRadius;

        public Transform Transform { get; private set; }

        protected override void OnCreate()
        {
            Transform = Parent.GetComp<Transform>();
        }
    }
}
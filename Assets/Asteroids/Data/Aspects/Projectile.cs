﻿using Asteroids.ECS;

namespace Asteroids.Data.Aspects
{
    public class Projectile : Comp<Projectile>
    {
        /// <summary>
        /// Time until bullet disappears
        /// </summary>
        public float timeLeft = 3f;

        public bool destroyOnImpact;

        public Transform Transform { get; private set; }
        public Velocity Velocity { get; private set; }

        protected override void OnCreate()
        {
            Transform = Parent.GetComp<Transform>();
            Velocity = Parent.GetComp<Velocity>();
        }
    }
}
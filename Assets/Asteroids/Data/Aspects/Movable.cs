﻿using Asteroids.ECS.Asteroids.ECS;

namespace Asteroids.Data.Aspects
{
    public class Movable : Comp<Movable>
    {
        public Transform Transform { get; private set; }
        public Velocity Velocity { get; private set; }

        protected override void OnCreate()
        {
            Transform = Parent.TryAddComp<Transform>();
            Velocity = Parent.TryAddComp<Velocity>();
        }

        public virtual void OnUpdate()
        {
            var delta = Time.delta;
            Transform.position += Velocity.forwardLinear * delta;
            Transform.angle += Velocity.angular * delta;
        }
    }
}
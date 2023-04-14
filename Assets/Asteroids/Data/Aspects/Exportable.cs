using Asteroids.ECS;

namespace Asteroids.Data.Aspects
{
    public class Exportable : Comp<Exportable>
    {
        public GraphicsDef Def { get; set; }
        public Transform Transform { get; private set; }

        protected override void OnCreate()
        {
            Transform = Parent.GetComp<Transform>();
        }
    }
}
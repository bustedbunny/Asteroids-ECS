using Asteroids.Data.Common;

namespace Asteroids.Data.Aspects
{
    public class Exportable : Comp<Exportable>
    {
        public GraphicsDef Def { get; private set; }
        public Transform Transform { get; private set; }

        public override void OnCreate()
        {
            Transform = Parent.GetComp<Transform>();
        }
    }
}
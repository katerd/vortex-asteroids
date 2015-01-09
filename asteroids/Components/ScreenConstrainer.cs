using SlimMath;
using Vortex.Scenegraph.Components;

namespace asteroids.Components
{
    public class ScreenConstrainer : ScriptComponent
    {
        public Vector3 Extents { get; set; }

        public override void OnFrame(double delta)
        {
            base.OnFrame(delta);

            var vx = Entity.RigidbodyComponent.Velocity.X;
            var vy = Entity.RigidbodyComponent.Velocity.Y;

            var x = Entity.LocalPosition.X;
            var y = Entity.LocalPosition.Y;

            var ex = Extents.X;
            var ey = Extents.Y;

            if ((x > ex && vx > 0) || (x < -ex && vx < 0))
            {
                vx = -vx;
            }

            if ((y > ey && vy > 0) || (y < -ey && vy < 0))
            {
                vy = -vy;
            }

            Entity.RigidbodyComponent.Velocity = new Vector3(vx, vy, 0);
        }
    }
}
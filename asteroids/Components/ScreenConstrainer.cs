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

            var newX = Entity.RigidbodyComponent.Velocity.X;
            var newY = Entity.RigidbodyComponent.Velocity.Y;

            if (Entity.LocalPosition.X > Extents.X || Entity.LocalPosition.X < -Extents.X)
            {
                newX = -newX;
            }

            if (Entity.LocalPosition.Y > Extents.Y || Entity.LocalPosition.Y < -Extents.Y)
            {
                newY = -newY;
            }

            Entity.RigidbodyComponent.Velocity = new Vector3(newX, newY, 0);
        }
    }
}
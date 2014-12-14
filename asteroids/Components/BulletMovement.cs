using SlimMath;
using Vortex.Scenegraph.Components;

namespace asteroids.Components
{
    public class BulletMovement : ScriptComponent
    {
        public Vector3 MovementVector { get; set; }
        public float MovementSpeed { get; set; }

        public override void OnUpdate(float delta)
        {
            base.OnUpdate(delta);
            Entity.LocalPosition += (MovementVector * MovementSpeed * delta);
        }
    }
}
using SlimMath;
using Vortex.Scenegraph.Components;

namespace asteroids.Components
{
    public class SmoothFollow : ScriptComponent
    {
        public override void Initialize()
        {
            base.Initialize();
            CameraComponent.Main = Entity.GetComponent<CameraComponent>();
            CameraComponent.Main.Zoom = 50;
        }

        public override void OnUpdate(float delta)
        {
            base.OnUpdate(delta);

            var ship = Scene.GetEntityWithComponent<ShipMovement>();

            if (ship == null)
                return;

            Entity.LocalPosition = Vector3.Lerp(ship.LocalPosition, Entity.LocalPosition, delta * 1.5f);
        }
    }
}
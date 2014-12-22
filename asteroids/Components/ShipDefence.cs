using Vortex.Core.Assets;
using Vortex.Graphics;
using Vortex.Scenegraph;
using Vortex.Scenegraph.Components;

namespace asteroids.Components
{
    public class ShipDefence : ScriptComponent
    {
        public int ShieldPoints { get; set; }
        public int HealthPoints { get; set; }
        public int MaximumHealthPoints { get; set; }
        public bool Immune { get; set; }

        public ShipDefence()
        {
            ShieldPoints = 0;
            HealthPoints = 100;
            MaximumHealthPoints = 100;
            Immune = false;
        }

        public override void OnUpdate(float delta)
        {
            base.OnUpdate(delta);

            var meshComponent = Entity.GetComponentInSelfOrChildren<MeshComponent>();
            if (meshComponent != null)
            {
                meshComponent.Material = Immune 
                    ? StaticAssetLoader.Get<Material>("Materials/shipImmune.material") 
                    :  StaticAssetLoader.Get<Material>("Materials/ship.material");
            }
        }

        public void MakeImmune()
        {
            if (Immune)
                return;

            Immune = true;

            InvokeDelayed(() => Immune = false, 5);
        }

        public override void OnCollisionEnter(RigidbodyCollision collision)
        {
            base.OnCollisionEnter(collision);

            if (Immune)
                return;

            var speed = collision.RelativeVelocity.Length;

            HealthPoints -= (int)(speed * 2.0f);
            if (HealthPoints < 0)
                HealthPoints = 0;

        }
    }
}
using System;
using SlimMath;
using Vortex.Core;
using Vortex.Core.Assets;
using Vortex.Core.Extensions;
using Vortex.Graphics;
using Vortex.Graphics.OpenGL;
using Vortex.Scenegraph;
using Vortex.Scenegraph.Components;

namespace asteroids.Components
{
    public class ShipDefence : ScriptComponent
    {
        private double _shieldPulsate;
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

        public override void Initialize()
        {
            base.Initialize();
            MakeImmune();
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

                if (Immune)
                {
                    meshComponent.Material.SetColor4("matAmbient", Colours.Blue.Multiply(0.5f + (0.5f) *(float)Math.Sin(_shieldPulsate)));
                    meshComponent.Material.SetColor4("matDiffuse", new Color4(1, 0, 0, 0));
                }
            }
            else
            {
                Log("Ship is immune but i can't change the material!");
            }

            _shieldPulsate += delta * 10;
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
            {
                HealthPoints = 0;
            }

        }
    }
}
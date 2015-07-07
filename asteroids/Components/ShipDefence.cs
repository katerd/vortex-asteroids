using System;
using asteroids.Enums;
using asteroids.Messaging;
using SlimMath;
using Vortex.Core;
using Vortex.Core.Assets;
using Vortex.Core.Extensions;
using Vortex.Graphics;
using Vortex.Scenegraph;
using Vortex.Scenegraph.Components;

namespace asteroids.Components
{
    public class ShipDefence : ScriptComponent
    {
        

        private double _shieldPulsate;
        public float ShieldPoints { get; set; }
        public float HealthPoints { get; set; }
        public float MaximumHealthPoints { get; set; }
        public float MaximumHealthRegenPoints { get; set; }
        public float HealthRegenerationPoolConsumptionRate { get; set; }
        public bool Immune { get; set; }
        public float HealthRegenerationPool { get; set; }

        public bool IsRegenerating
        {
            get { return HealthRegenerationPool > 0; }
        }

        public ShipDefence()
        {
            ShieldPoints = 0;
            HealthPoints = 200;
            MaximumHealthPoints = 250;
            MaximumHealthRegenPoints = 100;
            HealthRegenerationPool = 0;
            HealthRegenerationPoolConsumptionRate = 5f;
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
                    : IsRegenerating 
                        ? StaticAssetLoader.Get<Material>("Materials/shipRegenerating.material")
                        : StaticAssetLoader.Get<Material>("Materials/ship.material");

                if (Immune)
                {
                    meshComponent.Material.SetColor4("matAmbient", Colours.Blue.Multiply(0.5f + (0.5f) *(float)Math.Sin(_shieldPulsate)));
                    meshComponent.Material.SetColor4("matDiffuse", new Color4(1, 0, 0, 0));
                }
                else if (IsRegenerating)
                {
                    meshComponent.Material.SetColor4("matAmbient", Colours.LightGreen.Multiply(0.5f + (0.5f) * (float)Math.Sin(_shieldPulsate)));
                    meshComponent.Material.SetColor4("matDiffuse", new Color4(1, 0, 0, 0));
                }
            }
            else
            {
                Log("Ship is immune but i can't change the material!");
            }

            _shieldPulsate += delta * 10;

            if (HealthRegenerationPool > 0)
            {
                var amt = HealthRegenerationPool*HealthRegenerationPoolConsumptionRate*delta;

                if (amt < 1)
                {
                    amt = HealthRegenerationPool;
                }

                HealthRegenerationPool -= amt;
                HealthPoints += amt;
            }
        }

        public void RegenerateHealth()
        {
            if (Math.Abs(ShieldPoints - MaximumHealthPoints) < 0.001f)
                return;

            var maxRegen = Math.Min(MaximumHealthPoints - HealthPoints, MaximumHealthRegenPoints);
            HealthRegenerationPool = maxRegen;
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
                this.Dispatch(EventType.PlayerDestroyed);
            }

        }
    }
}
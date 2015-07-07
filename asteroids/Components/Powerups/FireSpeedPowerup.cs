using System;
using SlimMath;
using Vortex.Core;
using Vortex.Core.Extensions;
using Vortex.Scenegraph.Components;
using Vortex.Scenegraph.Components.Collision;
using Vortex.Scenegraph.Components.Gui;

namespace asteroids.Components.Powerups
{
    public class FireSpeedPowerup : Powerup
    {
        private double _cycle;
        private double _spawnTime;

        public const float Life = 10*1000.0f;// life in ms

        public override void Initialize()
        {
            base.Initialize();

            _spawnTime = Timer.GetTime();
        }

        public override void OnTriggerEnter(ColliderComponent other)
        {
            base.OnTriggerEnter(other);

            var shipFiring = other.Entity.GetComponent<ShipFiring>();
            if (shipFiring == null)
            {
                return;
            }
            shipFiring.ApplyFireSpeedPowerup();

            SpawnPowerupText();

            Entity.Destroy();
        }

        private void SpawnPowerupText()
        {
            var powTextEntity = Scene.CreateEntity();
            powTextEntity.Parent = Scene.GetEntityWithComponent<GuiRootComponent>();
            powTextEntity.LocalPosition = CameraComponent.Main.WorldToScreen(Entity.WorldPosition).AsVector3();
            var component = new PowText();
            powTextEntity.AddComponent(component);
            component.Text = "Fire speed increase!";
            component.TextColour = Colours.Magenta;
        }

        public override void OnUpdate(float delta)
        {
            base.OnUpdate(delta);

            var currentTime = Timer.GetTime();
            var lifeFactor = (float)((currentTime - _spawnTime)/Life);
            var lifeMultiplier = 2 + lifeFactor;

            _cycle += delta * (lifeMultiplier * lifeMultiplier * lifeMultiplier);
            if (_cycle > 3.141f)
            {
                _cycle -= 3.141f;
            }

            if (currentTime > _spawnTime + Life)
            {
                Entity.Destroy();
                return;
            }

            var mesh = Entity.GetComponentInSelfOrChildren<MeshComponent>();
            mesh.Material.SetColor4("matAmbient", new Color4(0.2f * lifeFactor, 1.0f, (float)Math.Sin(_cycle), 0.0f));

        }
    }
}
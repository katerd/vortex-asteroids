using asteroids.Components.Powerups;
using SlimMath;
using Vortex.Core;
using Vortex.Core.Assets;
using Vortex.Core.Extensions;
using Vortex.Graphics;
using Vortex.Scenegraph;
using Vortex.Scenegraph.Components;
using Vortex.Scenegraph.Utility;

namespace asteroids.Spawners
{
    public static class PowerupSpawner
    {
        public static void SpawnIn(Scene scene, Vector3 worldPosition)
        {
            var powerup = ColladaUtils.CreateEntity(scene, @"Models\asteroid-small.dae", true);
            powerup.AddComponent(CreateRandomPowerup());

            powerup.LocalPosition = worldPosition;

            var meshes = powerup.GetComponentsInSelfOrChildren<MeshComponent>();
            foreach (var mesh in meshes)
            {
                var material = StaticAssetLoader.Get<Material>("Materials/powerup.material");
                var clone = Material.Clone(material);
                mesh.Material = clone;
            }
        }

        private static Powerup CreateRandomPowerup()
        {
            if (StaticRng.Random.NextBool())
            {
                return new FireSpeedPowerup();
            }
            else
            {
                return new HealthRegenPowerup();
            }
        }
    }
}
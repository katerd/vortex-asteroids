using System;
using asteroids.Components;
using SlimMath;
using Vortex.Core;
using Vortex.Core.Assets;
using Vortex.Core.Collision;
using Vortex.Core.Extensions;
using Vortex.Graphics;
using Vortex.Scenegraph;
using Vortex.Scenegraph.Components;
using Vortex.Scenegraph.Components.Collision;
using Vortex.Scenegraph.Utility;

namespace asteroids.Spawners
{
    public static class AsteroidSpawner
    {
        private const float SpawnRange = 80.0f;
        private const float AsteroidSpeed = 4.0f;

        /// <summary>
        /// Spawn asteroid in random location.
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="size"></param>
        public static void SpawnIn(Scene scene, int size)
        {
            var randomX = -(SpawnRange * 0.5f) + (float)StaticRng.Random.NextDouble() * SpawnRange;
            var randomY = -(SpawnRange * 0.5f) + (float)StaticRng.Random.NextDouble() * SpawnRange;
            var position = new Vector3(randomX, randomY, 0);

            SpawnIn(scene, size, position);
        }

        /// <summary>
        /// Spawn asteroid at specified world coordinates.
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="size"></param>
        /// <param name="worldPosition"></param>
        public static void SpawnIn(Scene scene, int size, Vector3 worldPosition)
        {
            if (size > 3 || size < 1)
            {
                throw new ArgumentOutOfRangeException("size");
            }

            var asteroid = ColladaUtils.CreateEntity(scene, GetModelFilename(size), true);

            asteroid.TransformComponent.LocalPosition = worldPosition;

            asteroid.CreateComponent<Asteroid>(destructible =>
            {
                destructible.Hitpoints = 100 * size;
                destructible.Size = size;
            });

            asteroid.CreateComponent<RigidbodyComponent>(component =>
            {
                component.Mass = 30000 * size;

                var velocity = StaticRng.Random.NextVector3(new Vector3(-AsteroidSpeed, -AsteroidSpeed, 0), new Vector3(AsteroidSpeed, AsteroidSpeed, 0));

                component.Velocity = velocity;
                component.Drag = 0;
                component.PositionConstraint = new Vector3Constraints { X = false, Y = false, Z = true};
            });

            asteroid.CreateComponent<JsScriptComponent>(constrainer =>
            {
                constrainer.Source = StaticAssetLoader.GetString("screenConstrainer.js");
                constrainer.Properties.Extents = new Vector3(40, 30, 0);
            });
        }

        private static string GetModelFilename(int size)
        {
            switch (size)
            {
                case 3:
                    return @"Models\asteroid-large-textured.dae";
                case 2:
                    return @"Models\asteroid-medium-textured.dae";
                case 1:
                    return @"Models\asteroid-small-textured.dae";
                default:
                    throw new Exception(string.Format("Unknown asteroid size {0}", size));
            }
        }
    }
}
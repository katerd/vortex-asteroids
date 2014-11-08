using System;
using asteroids.Components;
using SlimMath;
using Vortex.Core;
using Vortex.Core.Collision;
using Vortex.Scenegraph;
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

            var asteroid = scene.CreateEntity(GetModelFilename(size), true);
            asteroid.LocalPosition = worldPosition;

            asteroid.CreateComponent<Destructible>(destructible =>
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

            asteroid.CreateComponent<ScreenConstrainer>(constrainer =>
            {
                constrainer.Extents = new Vector3(SpawnRange*0.5f, SpawnRange*0.5f, 0);
            });
        }

        private static string GetModelFilename(int size)
        {
            switch (size)
            {
                case 3:
                    return @"Models\asteroid-large.dae";
                case 2:
                    return @"Models\asteroid-medium.dae";
                case 1:
                    return @"Models\asteroid-small.dae";
                default:
                    throw new Exception(string.Format("Unknown asteroid size {0}", size));
            }
        }
    }
}
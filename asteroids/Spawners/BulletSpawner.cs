using asteroids.Components;
using SlimMath;
using Vortex.Core;
using Vortex.Core.Extensions;
using Vortex.Scenegraph;
using Vortex.Scenegraph.Components.Collision;
using Vortex.Scenegraph.Utility;

namespace asteroids.Spawners
{
    public static class BulletSpawner
    {
        public static void SpawnIn(Scene scene, Vector3 position, float angle, float bulletLife, float bulletSpeed, int damage)
        {
            var bullet = ColladaUtils.CreateEntity(scene, @"Models\bullet.dae");

            bullet.LocalPosition = position;

            var bulletMovement = new BulletMovement
            {
                MovementVector = VectorExtensions.From2DAngle(angle).NormalizeRet(),
                MovementSpeed = bulletSpeed
            };
            bullet.AddComponent(bulletMovement);

            var bulletLifeComponent = new KillAfterDuration { KillTime = Timer.GetTime() + (bulletLife * 1000) };
            bullet.AddComponent(bulletLifeComponent);

            var projectile = new Projectile { BaseDamage = damage };
            bullet.AddComponent(projectile);

            bullet.CreateComponent<SphereColliderComponent>(component => component.Radius = 0.8f);
        }
    }
}
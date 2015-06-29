using System.Runtime.InteropServices.ComTypes;
using asteroids.Components;
using SlimMath;
using Vortex.Core;
using Vortex.Core.Assets;
using Vortex.Core.Extensions;
using Vortex.Scenegraph;
using Vortex.Scenegraph.Components;
using Vortex.Scenegraph.Components.Collision;
using Vortex.Scenegraph.Utility;

namespace asteroids.Spawners
{
    public static class BulletSpawner
    {
        public static void SpawnIn(Scene scene, Vector3 position, float angle, float bulletLife, float bulletSpeed, int damage)
        {
            var bullet = ColladaUtils.CreateEntity(scene, @"Models\bullet.dae");

            var meshes = bullet.GetComponentsInSelfOrChildren<MeshComponent>();
            foreach (var mesh in meshes)
            {
                mesh.Mesh.Scale(8.0f);
            }
            var boundingSpheres = bullet.GetComponentsInSelfOrChildren<SphereColliderComponent>();
            foreach (var sphere in boundingSpheres)
            {
                sphere.Radius *= 8.0f;
            }

            bullet.LocalPosition = position;

            bullet.CreateComponent<JsScriptComponent>(component =>
            {
                component.Source = StaticAssetLoader.GetString("bulletMovement.js");
                component.Properties.MovementVector = VectorExtensions.From2DAngle(angle).NormalizeRet();
                component.Properties.MovementSpeed = bulletSpeed;
            });

            bullet.CreateComponent<JsScriptComponent>(component =>
            {
                component.Source = StaticAssetLoader.GetString("killAfterDuration.js");
                component.Properties.KillTime = Timer.GetTime() + (bulletLife*1000);
            });

            var projectile = new Projectile { BaseDamage = damage };
            bullet.AddComponent(projectile);

            bullet.CreateComponent<SphereColliderComponent>(component => component.Radius = 0.8f);
        }
    }
}
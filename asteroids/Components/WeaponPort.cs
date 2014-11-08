using asteroids.Spawners;
using Vortex.Core;
using Vortex.Scenegraph.Components;

namespace asteroids.Components
{
    public class WeaponPort : ScriptComponent
    {
        public float BulletLife { get; set; }
        public float BulletSpeed { get; set; }
        public int Damage { get; set; }

        private double _lastFireTime;
        private bool _shouldSpawnBullet;
        private int _weaponCooldown;

        public override void Initialize()
        {
            base.Initialize();
            _lastFireTime = 0;

            _weaponCooldown = 0;

            BulletLife = 2.0f;
            BulletSpeed = 40f;
            Damage = 100;
        }

        public void FireWeapon()
        {
            if (_lastFireTime + _weaponCooldown > Timer.GetTime())
            {
                return;
            }
            _shouldSpawnBullet = true;
        }

        public override void OnUpdate(float delta)
        {
            base.OnUpdate(delta);

            if (_shouldSpawnBullet)
            {
                SpawnBullet();
            }
            _shouldSpawnBullet = false;
        }

        private void SpawnBullet()
        {
            var parent = Entity.GetComponentInSelfOrParents<ShipFiring>().Entity;
            var angle = -parent.LocalRotation.Z;
            var position = Entity.WorldPosition;

            BulletSpawner.SpawnIn(Scene, position, angle, BulletLife, BulletSpeed, Damage);

            _lastFireTime = Timer.GetTime();
        }


    }


}
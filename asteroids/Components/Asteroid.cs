using asteroids.Enums;
using asteroids.Spawners;
using Vortex.Scenegraph.Components;
using Vortex.Scenegraph.Components.Collision;

namespace asteroids.Components
{
    public class Asteroid : ScriptComponent
    {
        public int Hitpoints { get; set; }
        public int Size { get; set; }

        private bool _hitByCriticalHit;

        public Asteroid()
        {
            _hitByCriticalHit = false;
        }

        public override void OnTriggerEnter(ColliderComponent other)
        {
            base.OnTriggerEnter(other);

            var projectile = other.Entity.GetComponent<Projectile>();
            if (projectile != null)
            {
                var damage = projectile.BaseDamage;
                TakeDamage(damage);
                projectile.HitObject();
            }
        }

        public void TakeDamage(int damage)
        {
            Hitpoints -= damage;
            if (Hitpoints < 0)
                Hitpoints = 0;
        }

        public void Nuke()
        {
            _hitByCriticalHit = true;
            Hitpoints = 0;
        }

        public override void OnUpdate(float delta)
        {
            base.OnUpdate(delta);

            if (Hitpoints <= 0)
            {
                var gameDirector = Scene.GetComponent<GameDirector>();

                Entity.Destroy();

                if (!_hitByCriticalHit)
                {
                    Log("Destructible, spawning asteroids");

                    if (Size == 3)
                    {
                        for (var i = 0; i < 3; i++)
                        {
                            gameDirector.SpawnAsteroid(Scene, Size - 1, Entity.WorldPosition);
                        }
                    }
                    else if (Size == 2)
                    {
                        for (var i = 0; i < 2; i++)
                        {
                            gameDirector.SpawnAsteroid(Scene, Size - 1, Entity.WorldPosition);
                        }
                    }
                }
                else
                {
                    Log("Critical hit smashes asteroid into a fine mist.");
                }

                gameDirector.HandleGameEvent(EventType.AsteroidDestroyed);
            }
        }
    }
}
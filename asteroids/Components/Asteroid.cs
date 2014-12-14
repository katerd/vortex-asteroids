using asteroids.Spawners;
using Vortex.Core.Logging;
using Vortex.Scenegraph.Components;
using Vortex.Scenegraph.Components.Collision;

namespace asteroids.Components
{
    public class Asteroid : ScriptComponent
    {
        public float Hitpoints { get; set; }
        public int Size { get; set; }

        public override void OnTriggerEnter(ColliderComponent other)
        {
            base.OnTriggerEnter(other);

            var projectile = other.Entity.GetComponent<Projectile>();
            if (projectile != null)
            {
                Hitpoints -= projectile.BaseDamage;
                projectile.HitObject();
            }
        }

        public override void OnUpdate(float delta)
        {
            base.OnUpdate(delta);

            if (Hitpoints <= 0)
            {
                Entity.Destroy();

                // crappy.
                Scene.EntityWithComponent<GameDirector>().GetComponent<GameDirector>().AsteroidDestroyed(this);


                Logger.Write("Destructible, spawning asteroids", LoggerLevel.Info);

                if (Size == 3)
                {
                    for (var i = 0; i < 3; i++)
                    {
                        AsteroidSpawner.SpawnIn(Scene, Size - 1, Entity.WorldPosition);
                    }
                }
                else if (Size == 2)
                {
                    for (var i = 0; i < 2; i++)
                    {
                        AsteroidSpawner.SpawnIn(Scene, Size - 1, Entity.WorldPosition);
                    }
                }
            }
        }
    }
}
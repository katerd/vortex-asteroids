using asteroids.Extensions;
using asteroids.Spawners;
using SlimMath;
using Vortex.Scenegraph;
using Vortex.Scenegraph.Components;

namespace asteroids.Components
{
    public class GameDirector : ScriptComponent
    {
        public int LivesRemaining { get; set; }
        public int CurrentLevel { get; private set; }
        public int AsteroidCount { get; private set; }

        private bool _deadTriggered;

        public override void Initialize()
        {
            base.Initialize();

            CurrentLevel = 0;
            LivesRemaining = 3;
            InvokeDelayed(DoPreLevel, 1);
        }

        private void DoPreLevel()
        {
            CurrentLevel++;
            StartLevel(CurrentLevel);
            InvokeDelayed(DoInLevel, 1);
        }

        public void StartLevel(int level)
        {
            CurrentLevel = level;
            
            // clear all existing asteroids
            var existingRoids = Scene.GetEntitiesWithComponent<Asteroid>();
            foreach (var roid in existingRoids)
            {
                roid.Destroy();
            }

            // create new ones!
            SpawnAsteroids();
            SpawnPlayer();
        }

        private void SpawnAsteroids()
        {
            var asteroidCount = 5 + (int) (CurrentLevel*4f);

            for (var i = 0; i < asteroidCount; i++)
            {
                AsteroidSpawner.SpawnIn(Scene, 3);
            }
            AsteroidCount += asteroidCount;
        }

        private void DoInLevel()
        {
            if (ShipDefence == null)
            {
                Log("Can't find ShipDefence component!");
                return;
            }

            if (ShipDefence.Entity.Active && ShipDefence.HealthPoints <= 0 && !_deadTriggered)
            {
                _deadTriggered = true;
                ShipDefence.Entity.Active = false;

                if (LivesRemaining > 0)
                {
                    InvokeDelayed(SpawnPlayer, 2);
                }
                else
                {
                    LivesRemaining = -1;
                    return;
                }
            }

            // get asteroid count.
            AsteroidCount = Scene.GetActiveAsteroidCount();

            if (AsteroidCount == 0)
            {
                InvokeDelayed(DoLevelComplete, 0);
            }
            else
            {
                InvokeDelayed(DoInLevel, 1);
            }
        }

        private void SpawnPlayer()
        {
            // remove old ship, and create a new one.
            if (ShipEntity != null)
                ShipEntity.Destroy();

            ShipSpawner.SpawnIn(Scene, new Vector3(-30, 0, 0));

            LivesRemaining -= 1;
            _deadTriggered = false;
        }

        private void DoLevelComplete()
        {
            InvokeDelayed(DoPreLevel, 3);
        }

        public void AsteroidDestroyed(Asteroid asteroid)
        {
            AsteroidCount = Scene.GetActiveAsteroidCount();
        }

        private Entity ShipEntity
        {
            get { return Scene.GetEntityWithComponent<ShipMovement>(); }
        }

        private ShipDefence ShipDefence
        {
            get
            {
                var shipEntity = ShipEntity;
                return shipEntity != null ? shipEntity.GetComponent<ShipDefence>() : null;
            }
        }
    }
}
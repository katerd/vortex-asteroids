using asteroids.Enums;
using asteroids.Messaging;
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
        public bool GameCompleted { get; set; }
        public int Score { get; set; }

        public void SpawnAsteroid(Scene scene, int size, Vector3 worldPosition)
        {
            AsteroidSpawner.SpawnIn(Scene, size, worldPosition);
            AsteroidCount++;
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

        public override void Initialize()
        {
            base.Initialize();

            this.SubscribeTo(EventType.AsteroidDestroyed, HandleAsteroidDestroyed);
            this.SubscribeTo(EventType.PlayerDestroyed, HandlePlayerDestroyed);

            GameCompleted = false;
            CurrentLevel = 0;
            LivesRemaining = 3;
            InvokeDelayed(DoPreLevel, 1);
        }

        private void DoPreLevel()
        {
            CurrentLevel++;
            StartLevel(CurrentLevel);
        }

        private void SpawnAsteroids()
        {
            var asteroidCount = 3 + (int) (CurrentLevel*2f);

            for (var i = 0; i < asteroidCount; i++)
            {
                AsteroidSpawner.SpawnIn(Scene, 3);
                AsteroidCount++;
            }
        }

        private void HandleAsteroidDestroyed(object messageId, object data)
        {
            Log(string.Format("Asteroid destroyed. count = {0}", AsteroidCount));

            AsteroidCount--;

            if (AsteroidCount == 0)
            {
                Log("Last asteroid destroyed!");
                InvokeDelayed(DoLevelComplete, 0);
            }
        }

        private void HandlePlayerDestroyed(object messageId, object data)
        {
            if (LivesRemaining > 0)
            {
                LivesRemaining--;
                InvokeDelayed(SpawnPlayer, 2);
            }
            else
            {
                LivesRemaining = -1;
            }
        }

        private void SpawnPlayer()
        {
            // remove old ship, and create a new one.
            var entity = Scene.GetEntityWithComponent<ShipMovement>();
            if (entity != null)
                entity.Destroy();

            ShipSpawner.SpawnIn(Scene, new Vector3(-30, 0, 0));
        }

        private void DoLevelComplete()
        {
            if (CurrentLevel == 5)
            {
                DoGameWon();
            }
            else
            {
                InvokeDelayed(DoPreLevel, 3);    
            }
        }

        private void DoGameWon()
        {
            GameCompleted = true;
        }
    }
}
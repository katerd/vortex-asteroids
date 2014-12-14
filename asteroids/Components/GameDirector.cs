using asteroids.Extensions;
using asteroids.Spawners;
using Vortex.Scenegraph.Components;

namespace asteroids.Components
{
    public class GameDirector : ScriptComponent
    {
        public int CurrentLevel { get; private set; }
        public int AsteroidCount { get; private set; }

        public override void Initialize()
        {
            base.Initialize();

            CurrentLevel = 0;
            InvokeDelayed(DoPreLevel, 1);
        }

        private void DoPreLevel()
        {
            CurrentLevel++;

            SpawnAsteroids();

            InvokeDelayed(DoInLevel, 1);
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

        private void DoLevelComplete()
        {
            InvokeDelayed(DoPreLevel, 3);
        }

        public void AsteroidDestroyed(Asteroid asteroid)
        {
            AsteroidCount = Scene.GetActiveAsteroidCount();
        }
    }
}
using System.Data.Common;
using asteroids.Components.Powerups;
using asteroids.Enums;
using asteroids.Messaging;
using asteroids.Spawners;
using SlimMath;
using Vortex.Core;
using Vortex.Core.Extensions;
using Vortex.Scenegraph;
using Vortex.Scenegraph.Components;
using Vortex.Scenegraph.Components.Gui;

namespace asteroids.Components
{
    public class GameDirector : ScriptComponent
    {
        private const float PowerupSpawnChance = 0.1f;

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

            var existingPowerups = Scene.GetEntitiesWithComponent<FireSpeedPowerup>();
            foreach (var powerup in existingPowerups)
            {
                powerup.Destroy();
            }

            // create new ones!
            SpawnAsteroids();

            if (level == 1)
            {
                SpawnPlayer();
            }
            else
            {
                ResetPlayer();
            }
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
            var worldPosition = ((Entity)data).WorldPosition;

            Log(string.Format("Asteroid destroyed. count = {0}", AsteroidCount));

            AsteroidCount--;

            if (AsteroidCount == 0)
            {
                Log("Last asteroid destroyed!");
                InvokeDelayed(DoLevelComplete, 0);
            }

            if (StaticRng.Random.EventHappens(PowerupSpawnChance))
            {
                PowerupSpawner.SpawnIn(Scene, worldPosition);
            }

            var guiRoot = Scene.GetComponent<GuiRootComponent>();

            var powEntity = Scene.CreateEntity("powRoot");
            guiRoot.Entity.AddChild(powEntity);

            var pow = powEntity.CreateComponent<PowText>();
            pow.Text = "AN ASTEROID WAS DESTROYED!";
            powEntity.LocalPosition = CameraComponent.Main.WorldToScreen(worldPosition).AsVector3();
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
            var shipEntity = Scene.GetEntityWithComponent<ShipMovement>();
            if (shipEntity != null)
                shipEntity.Destroy();

            shipEntity = ShipSpawner.SpawnIn(Scene, new Vector3(-30, 0, 0));

            var defence = shipEntity.GetComponentInSelfOrParents<ShipDefence>();
            defence.MakeImmune();
        }

        private void ResetPlayer()
        {
            var shipMovement = Scene.GetComponent<ShipMovement>();
            shipMovement.Stop();

            var shipEntity = shipMovement.Entity;
            shipEntity.LocalPosition = new Vector3(-30, 0, 0);
            var defence = shipEntity.GetComponentInSelfOrChildren<ShipDefence>();
            defence.MakeImmune();
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
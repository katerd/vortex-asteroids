using asteroids.Extensions;
using asteroids.Spawners;
using SlimMath;
using Vortex.Core.Logging;
using Vortex.Scenegraph;
using Vortex.Scenegraph.Components;

namespace asteroids.Components
{
    public class GameDirector : ScriptComponent
    {
        public int LivesRemaining { get; set; }

        public int CurrentLevel { get; private set; }
        public int AsteroidCount { get; private set; }

        private ShipDefence _shipDefence;
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
            if (_shipDefence == null)
            {
                _shipDefence = FindShipDefenceComponent();
            }

            if (_shipDefence == null)
            {
                Logger.Write("Can't find ship defence component!?", LoggerLevel.Error);
                return;
            }

            if (_shipDefence.HealthPoints <= 0 && !_deadTriggered)
            {
                _deadTriggered = true;
                _shipDefence.Entity.Active = false;

                if (LivesRemaining > 0)
                {
                    InvokeDelayed(RespawnPlayer, 2);
                }
                else
                {
                    // todo: show replay button
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

        private Entity ShipEntity
        {
            get { return Scene.GetEntityWithComponent<ShipMovement>(); }
        }

        private void RespawnPlayer()
        {
            var ship = ShipEntity;
            ship.Active = true;
            ship.LocalPosition = new Vector3(-30, 0, 0);
            ship.RigidbodyComponent.Velocity = new Vector3();
            ship.LocalRotation = ship.LocalRotation.Scale(1, 1, 0);

            var defence = ship.GetComponent<ShipDefence>();
            defence.HealthPoints = defence.MaximumHealthPoints;
            defence.MakeImmune();

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

        private ShipDefence FindShipDefenceComponent()
        {
            var shipEntity = ShipEntity;
            return shipEntity != null ? shipEntity.GetComponent<ShipDefence>() : null;
        }
    }
}
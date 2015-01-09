using Vortex.Scenegraph.Components;
using Vortex.Scenegraph.Components.Gui;

namespace asteroids.Components
{
    public class HudController : ScriptComponent
    {
        public LabelWidgetComponent StatusLabel { get; set; }
        public ImageWidgetComponent ShipHealth { get; set; }

        private GameDirector _gameDirector;
        private ShipDefence _shipDefence;

        public override void OnUpdate(float delta)
        {
            base.OnUpdate(delta);

            if (_shipDefence == null)
            {
                _shipDefence = FindShipDefenceComponent();
            }

            if (_gameDirector == null)
            {
                _gameDirector = Scene.GetComponent<GameDirector>();
            }

            StatusLabel.Text = GetLabelText();
            ShipHealth.HorizontalCrop = GetShipHealthPercentage();
        }

        private ShipDefence FindShipDefenceComponent()
        {
            // Get component attached to the one the player is controlling.
            var entity = Scene.GetEntityWithComponent<ShipMovement>();
            return entity != null ? entity.GetComponent<ShipDefence>() : null;
        }

        private float GetShipHealthPercentage()
        {
            if (_shipDefence == null)
                return 0;
            return _shipDefence.HealthPoints/(float)_shipDefence.MaximumHealthPoints;
        }

        private string GetLabelText()
        {
            if (_gameDirector == null)
                return string.Empty;

            if (_gameDirector.LivesRemaining == 0)
            {
                return string.Format("Game Over Man! Game Over!");
            }

            if (_shipDefence == null)
                return string.Empty;

            if (_shipDefence.HealthPoints == 0)
            {
                return string.Format("You died.");
            }

            return string.Format("Current level: {1}, asteroids: {0}, lives: {2}", 
                _gameDirector.AsteroidCount,
                _gameDirector.CurrentLevel,
                _gameDirector.LivesRemaining);
        }
    }
}
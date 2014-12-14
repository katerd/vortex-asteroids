using Vortex.Scenegraph.Components;
using Vortex.Scenegraph.Components.Gui;

namespace asteroids.Components
{
    public class HudController : ScriptComponent
    {
        public GameDirector GameDirector { get; set; }
        public ShipDefence ShipDefence { get; set; }
        public LabelWidgetComponent StatusLabel { get; set; }
        public ImageWidgetComponent ShipHealth { get; set; }

        public override void OnUpdate(float delta)
        {
            base.OnUpdate(delta);

            if (ShipDefence == null)
            {
                ShipDefence = FindShipDefenceComponent();
            }

            StatusLabel.Text = GetLabelText();
            ShipHealth.HorizontalCrop = GetShipHealthPercentage();
        }

        private ShipDefence FindShipDefenceComponent()
        {
            var entity = Scene.EntityWithComponent<ShipDefence>();
            return entity != null ? entity.GetComponent<ShipDefence>() : null;
        }

        private float GetShipHealthPercentage()
        {
            if (ShipDefence == null)
                return 0;
            return ShipDefence.HealthPoints/(float)ShipDefence.MaximumHealthPoints;
        }

        private string GetLabelText()
        {
            if (GameDirector == null)
                return string.Empty;

            return string.Format("Current level: {1}, Remaining asteroids: {0}", 
                GameDirector.AsteroidCount,
                GameDirector.CurrentLevel);
        }
    }
}
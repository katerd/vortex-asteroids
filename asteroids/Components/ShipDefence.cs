using Vortex.Scenegraph.Components;

namespace asteroids.Components
{
    public class ShipDefence : ScriptComponent
    {
        public int ShieldPoints { get; set; }
        public int HealthPoints { get; set; }
        public int MaximumHealthPoints { get; set; }

        public ShipDefence()
        {
            ShieldPoints = 0;
            HealthPoints = 100;
            MaximumHealthPoints = 100;
        }
    }
}
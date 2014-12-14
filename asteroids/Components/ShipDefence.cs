using Vortex.Scenegraph;
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

        public override void OnCollisionEnter(RigidbodyCollision collision)
        {
            base.OnCollisionEnter(collision);

            var speed = collision.RelativeVelocity.Length;

            HealthPoints -= (int)(speed * 2.0f);
            if (HealthPoints < 0)
                HealthPoints = 0;

        }
    }
}
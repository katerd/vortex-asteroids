using Vortex.Scenegraph.Components;

namespace asteroids.Components
{
    public class Projectile : ScriptComponent
    {
        public float BaseDamage { get; set; }

        public void HitObject()
        {
            Entity.Destroy();
        }
    }
}
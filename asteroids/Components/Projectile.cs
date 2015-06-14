using Vortex.Scenegraph.Components;

namespace asteroids.Components
{
    public class Projectile : ScriptComponent
    {
        public int BaseDamage { get; set; }

        public void HitObject()
        {
            Entity.Destroy();
        }
    }
}
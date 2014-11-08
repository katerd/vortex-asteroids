using Vortex.Core;
using Vortex.Scenegraph.Components;

namespace asteroids.Components
{
    public class KillAfterDuration : ScriptComponent
    {
        public double KillTime { get; set; }

        public override void OnUpdate(float delta)
        {
            base.OnUpdate(delta);

            if (Timer.GetTime() >= KillTime)
            {
                Entity.Destroy();
            }
        }
    }
}
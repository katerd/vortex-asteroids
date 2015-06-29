using System.Windows.Forms;
using Vortex.Scenegraph.Components;
using Timer = Vortex.Core.Timer;

namespace asteroids.Components
{
    public class ShipFiring : ScriptComponent
    {
        private double _lastFireTime;
        public bool IsFiring { get; set; }

        /// <summary>
        /// Rate in shots per second
        /// </summary>
        public float FireRate { get; set; }

        public ShipFiring()
        {
            FireRate = 2;
            IsFiring = false;
        }

        public override void OnKeyUp(Keys keyCode)
        {
            base.OnKeyUp(keyCode);

            if (keyCode == Keys.Space)
            {
                IsFiring = false;
            }
        }

        public override void OnKeyDown(Keys keyCode)
        {
            base.OnKeyDown(keyCode);

            if (keyCode == Keys.Space)
            {
                IsFiring = true;
            }
        }

        public override void OnUpdate(float delta)
        {
            base.OnUpdate(delta);

            if (IsFiring)
            {
                if (Timer.GetTime() > _lastFireTime + (1000.0f/FireRate))
                {
                    FireWeapons();
                    _lastFireTime = Timer.GetTime();
                }
            }
        }

        private void FireWeapons()
        {
            foreach (var weaponPort in Entity.GetComponentsInSelfOrChildren<WeaponPort>())
            {
                weaponPort.FireWeapon();
            }
        }
    }
}
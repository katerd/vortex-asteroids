using System.Windows.Forms;
using Vortex.Scenegraph.Components;

namespace asteroids.Components
{
    public class ShipFiring : ScriptComponent
    {
        public override void OnKeyDown(Keys keyCode)
        {
            base.OnKeyDown(keyCode);

            if (keyCode == Keys.Space)
            {
                FireWeapons();
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
using _Project.Scripts.Player;
using _Project.Scripts.UI;
using UnityEngine.InputSystem;

namespace _Project.Scripts.PlayerWeapons
{
    public class WeaponTrigger
    {
        private HUD_DataModel _hudModel;
        private ShipLaserAttack _shipLaserAttack;
        private ShipMissilesAttack _shipMissilesAttack;

        public WeaponTrigger(HUD_DataModel hudModel, ShipLaserAttack shipLaserAttack, ShipMissilesAttack shipMissilesAttack)
        {
            _hudModel = hudModel;
            _shipLaserAttack = shipLaserAttack;
            _shipMissilesAttack = shipMissilesAttack;
        }

        public void ShootMissile(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _shipMissilesAttack.PerformShot();
            }
        }

        public void ShootLaser(InputAction.CallbackContext context)
        {
            if (context.performed && _hudModel.CanShootLaser)
            {
                _shipLaserAttack.PerformShot();
            }
        }
    }
}
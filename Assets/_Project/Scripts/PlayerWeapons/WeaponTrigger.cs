using _Project.Scripts.Player;
using _Project.Scripts.UI;
using System;
using UnityEngine.InputSystem;

namespace _Project.Scripts.PlayerWeapons
{
    public class WeaponTrigger
    {
        private HudModel _hudModel;
        private ShipLaserAttack _shipLaserAttack;
        private ShipMissilesAttack _shipMissilesAttack;

        private int _laserShotsTotal;
        private int _missilesShotsTotal;

        public int LaserShotsTotal => _laserShotsTotal;
        public int MissilesShotsTotal => _missilesShotsTotal;

        public event Action LaserShot;

        public WeaponTrigger(HudModel hudModel, ShipLaserAttack shipLaserAttack, ShipMissilesAttack shipMissilesAttack)
        {
            _hudModel = hudModel;
            _shipLaserAttack = shipLaserAttack;
            _shipMissilesAttack = shipMissilesAttack;

            _laserShotsTotal = 0;
            _missilesShotsTotal = 0;
        }

        public void ShootMissile(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _missilesShotsTotal++;
                _shipMissilesAttack.PerformShot();
            }
        }

        public void ShootLaser(InputAction.CallbackContext context)
        {
            if (context.performed && _hudModel.CanShootLaser)
            {
                _laserShotsTotal++;
                LaserShot?.Invoke();
                _shipLaserAttack.PerformShot();
                _hudModel.ChangeLaserShotsCount(-1);
            }
        }
    }
}
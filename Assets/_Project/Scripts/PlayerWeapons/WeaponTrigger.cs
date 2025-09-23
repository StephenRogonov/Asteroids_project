using _Project.Scripts.GameFlow;
using _Project.Scripts.Player;
using _Project.Scripts.UI;
using System;

namespace _Project.Scripts.PlayerWeapons
{
    public class WeaponTrigger : IPause
    {
        private HudPresenter _hudPresenter;
        private ShipLaserAttack _shipLaserAttack;
        private ShipMissilesAttack _shipMissilesAttack;
        private PauseHandler _pauseHandler;

        private bool _isPaused;

        public event Action MissileShot;
        public event Action LaserShot;

        public WeaponTrigger(HudPresenter hudController, 
            ShipLaserAttack shipLaserAttack, 
            ShipMissilesAttack shipMissilesAttack, 
            PauseHandler pauseHandler)
        {
            _hudPresenter = hudController;
            _shipLaserAttack = shipLaserAttack;
            _shipMissilesAttack = shipMissilesAttack;
            _pauseHandler = pauseHandler;

            _pauseHandler.Add(this);
        }

        public void ShootMissile()
        {
            if (_isPaused == false)
            {
                MissileShot?.Invoke();
                _shipMissilesAttack.PerformShot();
            }
        }

        public void ShootLaser()
        {
            if (_hudPresenter.CanShootLaser() && _isPaused == false)
            {
                LaserShot?.Invoke();
                _shipLaserAttack.PerformShot();
                _hudPresenter.LaserShotsChanged(-1);
            }
        }

        public void Pause()
        {
            _isPaused = true;
        }

        public void Unpause()
        {
            _isPaused = false;
        }
    }
}
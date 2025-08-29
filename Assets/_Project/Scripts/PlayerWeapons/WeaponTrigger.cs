using _Project.Scripts.Bootstrap.Analytics;
using _Project.Scripts.GameFlow;
using _Project.Scripts.Player;
using _Project.Scripts.UI;

namespace _Project.Scripts.PlayerWeapons
{
    public class WeaponTrigger : IPause
    {
        private HudPresenter _hudPresenter;
        private ShipLaserAttack _shipLaserAttack;
        private ShipMissilesAttack _shipMissilesAttack;
        private AnalyticsEventManager _analyticsEventManager;
        private PauseHandler _pauseHandler;

        private bool _isPaused;

        public WeaponTrigger(HudPresenter hudController, 
            ShipLaserAttack shipLaserAttack, 
            ShipMissilesAttack shipMissilesAttack, 
            AnalyticsEventManager analyticsEventManager,
            PauseHandler pauseHandler)
        {
            _hudPresenter = hudController;
            _shipLaserAttack = shipLaserAttack;
            _shipMissilesAttack = shipMissilesAttack;
            _analyticsEventManager = analyticsEventManager;
            _pauseHandler = pauseHandler;

            _pauseHandler.Add(this);
        }

        public void ShootMissile()
        {
            if (_isPaused == false)
            {
                _analyticsEventManager.IncrementParameter(LogParameters.MissilesShotsTotal);
                _shipMissilesAttack.PerformShot();
            }
        }

        public void ShootLaser()
        {
            if (_hudPresenter.CanShootLaser() && _isPaused == false)
            {
                _analyticsEventManager.IncrementParameter(LogParameters.LaserShotsTotal);
                _analyticsEventManager.LogEventWithoutParameters(LoggingEvents.LASER_SHOT);
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
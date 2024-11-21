using System;

namespace _Project.Scripts.UI
{
    public class HUD_Controller
    {
        private HUD_DataModel _model;
        private HUD_View _view;
        private ShipShootingLaserConfig _laserConfig;
        private CountdownTimer _timer;

        public HUD_Controller(HUD_DataModel model, HUD_View view, ShipShootingLaserConfig laserConfig)
        {
            _model = model;
            _view = view;
            _laserConfig = laserConfig;

            _timer = new CountdownTimer();
            _timer.Reset(_laserConfig.LaserShotRestorationTime);

            _model.ChangeLaserShotsCount(_laserConfig.LaserShotsStartCount);
            _view = view;
        }

        public void Update(float deltaTime)
        {
            _timer.Tick(deltaTime);
            _view.DisplayLaserRestorationTime(TimeSpan.FromSeconds(_timer.RemainingTime).ToString("mm':'ss"));

            if (_timer.RemainingTime < 0)
            {
                _model.ChangeLaserShotsCount(1);
                _timer.Reset(_laserConfig.LaserShotRestorationTime);
            }
        }
    }
}
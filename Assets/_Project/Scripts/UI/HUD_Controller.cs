namespace _Project.Scripts.UI
{
    public class HUD_Controller
    {
        private HUD_DataModel _model;
        private ShipShootingLaserConfig _laserConfig;
        private CountdownTimer _timer;

        public HUD_Controller(HUD_DataModel model, ShipShootingLaserConfig laserConfig)
        {
            _model = model;
            _laserConfig = laserConfig;

            _timer = new CountdownTimer();
            _timer.Reset(_laserConfig.LaserShotRestorationTime);

            ChangeLaserShotsCount(_laserConfig.LaserShotsStartCount);
        }

        public void Update(float deltaTime)
        {
            _timer.Tick(deltaTime);
            //TODO Send remaining time to model
            //_view.DisplayLaserRestorationTime(TimeSpan.FromSeconds(_timer.RemainingTime).ToString("mm':'ss"));

            if (_timer.RemainingTime < 0)
            {
                ChangeLaserShotsCount(1);
                _timer.Reset(_laserConfig.LaserShotRestorationTime);
            }
        }

        private void ChangeLaserShotsCount(int count)
        {
            _model.SetLaserShots(_model.LaserShotsAvailable + count);
        }
    }
}
using _Project.Scripts.Player;
using System;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.UI
{
    public class HudController : ITickable
    {
        private HudModel _model;
        private HudView _view;
        private ShipLaserConfig _laserConfig;
        private ShipMovement _shipMovement;

        private CountdownTimer _timer;

        private string _shipPosition;
        private int _shipRotation;
        private float _shipSpeed;

        public HudController(HudModel model, HudView view, ShipLaserConfig laserConfig, ShipMovement shipMovement)
        {
            _model = model;
            _view = view;
            _laserConfig = laserConfig;
            _shipMovement = shipMovement;

            _timer = new CountdownTimer();
            _timer.Reset(_laserConfig.LaserShotRestorationTime);

            _model.ChangeLaserShotsCount(_laserConfig.LaserShotsStartCount);
            _view = view;
        }

        public void Tick()
        {
            CalculateShipStats();

            _timer.Tick(Time.deltaTime);
            _view.DisplayLaserRestorationTime(TimeSpan.FromSeconds(_timer.RemainingTime).ToString("mm':'ss"));

            if (_timer.RemainingTime < 0)
            {
                _model.ChangeLaserShotsCount(1);
                _timer.Reset(_laserConfig.LaserShotRestorationTime);
            }
        }

        public void CalculateShipStats()
        {
            _shipPosition = string.Format("{0}, {1}", Mathf.Round(_shipMovement.Position.x * 100f) / 100f,
                    Mathf.Round(_shipMovement.Position.y * 100f) / 100f);
            _shipRotation = Convert.ToInt32(_shipMovement.Rotation.z) % 360;
            _shipSpeed = Mathf.Round(Vector3.Magnitude(_shipMovement.Rigidbody.linearVelocity) * 100f) / 100f;

            _view.DisplayShipStats(_shipPosition, _shipRotation, _shipSpeed);
        }
    }
}
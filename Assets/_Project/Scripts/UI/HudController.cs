using _Project.Scripts.Common;
using _Project.Scripts.Configs;
using _Project.Scripts.Player;
using System;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.UI
{
    public class HudController : ITickable, IPause
    {
        private HudModel _model;
        private HudView _view;
        private RemoteConfig _remoteConfig;
        private ShipMovement _shipMovement;
        private PauseHandler _pauseHandler;

        private CountdownTimer _timer;

        private string _shipPosition;
        private int _shipRotation;
        private float _shipSpeed;

        private bool _isPaused;

        public HudController(HudModel model, HudView view, RemoteConfig remoteConfig, ShipMovement shipMovement, PauseHandler pauseHandler)
        {
            _model = model;
            _view = view;
            _remoteConfig = remoteConfig;
            _shipMovement = shipMovement;
            _pauseHandler = pauseHandler;

            _pauseHandler.Add(this);

            _timer = new CountdownTimer();
            _timer.Reset(_remoteConfig.LaserShotRestorationTime);

            _model.ChangeLaserShotsCount(_remoteConfig.LaserShotsStartCount);
            _view = view;
        }

        public void Tick()
        {
            if (_isPaused == false)
            {
                CalculateShipStats();

                _timer.Tick(Time.deltaTime);
                _view.DisplayLaserRestorationTime(TimeSpan.FromSeconds(_timer.RemainingTime).ToString("mm':'ss"));

                if (_timer.RemainingTime < 0)
                {
                    _model.ChangeLaserShotsCount(1);
                    _timer.Reset(_remoteConfig.LaserShotRestorationTime);
                }
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
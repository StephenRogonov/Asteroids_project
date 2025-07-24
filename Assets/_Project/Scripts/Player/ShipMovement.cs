using _Project.Scripts.Bootstrap.Analytics;
using _Project.Scripts.Bootstrap.Configs;
using _Project.Scripts.GameFlow;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Player
{
    public class ShipMovement : MonoBehaviour, IPause
    {
        private AnalyticsEventManager _analyticsEventManager;
        private GameConfig _remoteConfig;

        private float _acceleration;
        private float _maxSpeed;
        private float _rotationSpeed;

        private bool _isMoving;
        private float _rotateDirection;
        private Rigidbody2D _rigidbody;

        private PauseHandler _pauseHandler;

        private Vector2 _linearVelocity;
        private float _angularVelocity;
        private bool _isPaused;

        public Vector3 Position => transform.position;
        public Vector3 Rotation => transform.eulerAngles;
        public Rigidbody2D Rigidbody => _rigidbody;

        [Inject]
        private void Construct(GameConfig remoteConfig, AnalyticsEventManager analyticsEventManager, PauseHandler pauseHandler)
        {
            _analyticsEventManager = analyticsEventManager;
            _remoteConfig = remoteConfig;
            _pauseHandler = pauseHandler;
            _pauseHandler.Add(this);

            _acceleration = _remoteConfig.ShipAcceleration;
            _maxSpeed = _remoteConfig.ShipMaxSpeed;
            _rotationSpeed = _remoteConfig.ShipRotationSpeed;
        }

        public void Move(bool isMoving)
        {
            _isMoving = isMoving;
        }

        public void Rotate(float rotateDirection)
        {
            _rotateDirection = rotateDirection;
        }

        public void Pause()
        {
            _linearVelocity = _rigidbody.linearVelocity;
            _angularVelocity = _rigidbody.angularVelocity;
            _isPaused = true;
            _rigidbody.bodyType = RigidbodyType2D.Static;
        }

        public void Unpause()
        {
            _rigidbody.bodyType = RigidbodyType2D.Dynamic;
            _isPaused = false;
            _rigidbody.linearVelocity = _linearVelocity;
            _rigidbody.angularVelocity = _angularVelocity;
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _analyticsEventManager.LogEventWithoutParameters(LoggingEvents.START_GAME);
        }

        private void FixedUpdate()
        {
            MoveCharacter();
            RotateCharacter();
        }

        private void MoveCharacter()
        {
            if (_isMoving & _isPaused == false)
            {
                _rigidbody.AddForce(transform.up * _acceleration);
                _rigidbody.linearVelocity = Vector2.ClampMagnitude(_rigidbody.linearVelocity, _maxSpeed);
            }
        }

        private void RotateCharacter()
        {
            if (_rotateDirection != 0f)
            {
                _rigidbody.AddTorque(_rotateDirection * _rotationSpeed);
            }
        }
    }
}
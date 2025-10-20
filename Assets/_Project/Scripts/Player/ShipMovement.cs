using _Project.Scripts.Bootstrap.Configs;
using _Project.Scripts.DataPersistence;
using _Project.Scripts.GameFlow;
using System;
using UnityEngine;

namespace _Project.Scripts.Player
{
    public class ShipMovement : MonoBehaviour, IPause
    {
        private GameConfig _gameConfig;
        private PauseSwitcher _pauseSwitcher;

        private float _acceleration;
        private float _maxSpeed;
        private float _rotationSpeed;

        private bool _isMoving;
        private float _rotateDirection;
        private Rigidbody2D _rigidbody;

        private Vector2 _linearVelocity;
        private float _angularVelocity;
        private bool _isPaused;

        public Vector3 Position => transform.position;
        public Vector3 Rotation => transform.eulerAngles;
        public Rigidbody2D Rigidbody => _rigidbody;

        public event Action GameStarted;

        public void Init(DataPersistenceHandler dataPersistenceHandler, PauseSwitcher pauseSwitcher)
        {
            _gameConfig = dataPersistenceHandler.GameConfig;
            _pauseSwitcher = pauseSwitcher;
            _pauseSwitcher.Add(this);

            _acceleration = _gameConfig.ShipAcceleration;
            _maxSpeed = _gameConfig.ShipMaxSpeed;
            _rotationSpeed = _gameConfig.ShipRotationSpeed;
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
            GameStarted?.Invoke();
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

        public void ActivateObject()
        {
            gameObject.SetActive(true);
        }

        public void DeactivateObject()
        {
            gameObject.SetActive(false);
        }
    }
}
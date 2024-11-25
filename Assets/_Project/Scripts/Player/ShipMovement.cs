using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace _Project.Scripts.Player
{
    public class ShipMovement : MonoBehaviour
    {
        private float _acceleration;
        private float _maxSpeed;
        private float _rotationSpeed;

        private bool _isMoving;
        private float _rotateDirection;
        private Rigidbody2D _rigidbody;

        public Vector3 Position => transform.position;
        public Vector3 Rotation => transform.eulerAngles;
        public Rigidbody2D Rigidbody => _rigidbody;

        [Inject]
        private void Construct(ShipMovementConfig shipMovementConfig)
        {
            _acceleration = shipMovementConfig.Acceleration;
            _maxSpeed = shipMovementConfig.MaxSpeed;
            _rotationSpeed = shipMovementConfig.RotationSpeed;
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            MoveCharacter();
            RotateCharacter();
        }

        public void Move(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _isMoving = true;
            }
            else if (context.canceled)
            {
                _isMoving = false;
            }
        }

        public void PlayerRotateLeft(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _rotateDirection = 1f;
            }
            else if (context.canceled)
            {
                _rotateDirection = 0f;
            }
        }

        public void PlayerRotateRight(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _rotateDirection = -1f;
            }
            else if (context.canceled)
            {
                _rotateDirection = 0f;
            }
        }

        private void MoveCharacter()
        {
            if (_isMoving)
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
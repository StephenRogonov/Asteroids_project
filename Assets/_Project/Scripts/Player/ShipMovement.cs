using UnityEngine;
using UnityEngine.InputSystem;

namespace _Project.Scripts.Player
{
    public class ShipMovement : MonoBehaviour
    {
        [Header("Player Movement Variables")]
        [SerializeField] private float _acceleration = 5f;
        [SerializeField] private float _maxSpeed = 5f;
        [SerializeField] private float _rotationSpeed = 2f;

        private bool _isMoving;
        private float _rotateDirection;
        private Rigidbody2D _rigidbody;

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
                _rigidbody.velocity = Vector2.ClampMagnitude(_rigidbody.velocity, _maxSpeed);
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
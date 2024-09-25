using _Project.Scripts.Enemy;
using _Project.Scripts.Obstacles;
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

        [SerializeField] private GameObject _gameOverPanel;

        private bool _isMoving;
        private float _rotateDirection;
        private Rigidbody2D _rigidbody;
        private PlayerInputControls _inputActions;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _inputActions = new PlayerInputControls();
        }

        private void OnEnable()
        {
            _inputActions.Enable();

            _inputActions.Player.MoveForward.performed += Move;
            _inputActions.Player.MoveForward.canceled += Move;

            _inputActions.Player.RotateLeft.performed += PlayerRotateLeft;
            _inputActions.Player.RotateLeft.canceled += PlayerRotateLeft;

            _inputActions.Player.RotateRight.performed += PlayerRotateRight;
            _inputActions.Player.RotateRight.canceled += PlayerRotateRight;
        }

        private void OnDisable()
        {
            _inputActions.Disable();

            _inputActions.Player.MoveForward.performed -= Move;
            _inputActions.Player.MoveForward.canceled -= Move;

            _inputActions.Player.RotateLeft.performed -= PlayerRotateLeft;
            _inputActions.Player.RotateLeft.canceled -= PlayerRotateLeft;

            _inputActions.Player.RotateRight.performed -= PlayerRotateRight;
            _inputActions.Player.RotateRight.canceled -= PlayerRotateRight;
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

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.GetComponent<Asteroid>() != null || collision.gameObject.GetComponent<EnemyMovement>() != null)
            {
                _gameOverPanel.SetActive(true);
                DisableOnGameOver();
            }
        }

        private void DisableOnGameOver()
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = 0f;

            gameObject.SetActive(false);
        }
    }
}
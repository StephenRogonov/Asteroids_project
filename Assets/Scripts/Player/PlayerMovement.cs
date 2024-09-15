using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement Variables")]
    [SerializeField] private float acceleration = 5f;
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float rotationSpeed = 2f;

    [SerializeField] private TMP_Text playerPositionText;
    [SerializeField] private TMP_Text playerRotationText;
    [SerializeField] private TMP_Text playerSpeedText;

    [SerializeField] private GameObject gameOverPanel;

    private bool _isMoving;
    private float _rotateDirection;
    private Rigidbody2D _rigidbody;
    private InputSystem_Actions _inputActions;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _inputActions = new InputSystem_Actions();
    }

    private void Update()
    {
        playerPositionText.text = "Position: " + string.Format("{0}, {1}", transform.position.x, transform.position.y);
        playerRotationText.text = "Rotation: " + (Convert.ToInt32(transform.eulerAngles.z) % 360);
        playerSpeedText.text = "Speed: " + Vector3.Magnitude(_rigidbody.velocity);
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
            _rigidbody.AddForce(transform.up * acceleration);
            _rigidbody.velocity = Vector2.ClampMagnitude(_rigidbody.velocity, maxSpeed);
        }
    }

    private void RotateCharacter()
    {
        if (_rotateDirection != 0f)
        {
            _rigidbody.AddTorque(_rotateDirection * rotationSpeed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Asteroid"))
        {
            gameOverPanel.SetActive(true);
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

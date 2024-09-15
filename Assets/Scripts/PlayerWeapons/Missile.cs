using System;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 500f;
    [SerializeField] private float destroyTime = 5f;

    private Rigidbody2D _rigidbody;
    private Vector2 _screenPosition;
    private Camera _mainCamera;

    private float _rightSideOfTheScreen;
    private float _leftSideOfTheScreen;
    private float _topOfTheScreen;
    private float _bottomOfTheScreen;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _mainCamera = Camera.main;

        _rightSideOfTheScreen = _mainCamera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)).x;
        _leftSideOfTheScreen = _mainCamera.ScreenToWorldPoint(new Vector2(0f, 0f)).x;
        _topOfTheScreen = _mainCamera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)).y;
        _bottomOfTheScreen = _mainCamera.ScreenToWorldPoint(new Vector2(0f, 0f)).y;
    }

    private void Start()
    {
        Move();
        Destroy(gameObject, destroyTime);
    }

    private void Update()
    {
        DestroyOutOfScreenBounds();
    }

    private void DestroyOutOfScreenBounds()
    {
        _screenPosition = _mainCamera.WorldToScreenPoint(transform.position);

        if (_screenPosition.x <= 0 || _screenPosition.x >= Screen.width ||
            _screenPosition.y >= Screen.height || _screenPosition.y <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Move()
    {
        _rigidbody.AddForce(transform.up * bulletSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}

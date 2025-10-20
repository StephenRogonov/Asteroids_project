using _Project.Scripts.GameFlow;
using _Project.Scripts.Obstacles;
using System;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.PlayerWeapons
{
    public class Missile : MonoBehaviour, IPause
    {
        [SerializeField] private float _bulletSpeed = 500f;

        private Rigidbody2D _rigidbody;
        private Vector2 _screenPosition;
        private Camera _mainCamera;
        private PauseSwitcher _pauseHandler;
        private Vector2 _linearVelocity;

        public event Action<Missile> Destroyed;
        public event Action<IDamageable> ObstacleHit;

        [Inject]
        private void Construct(Camera camera, PauseSwitcher pauseHandler)
        {
            _mainCamera = camera;
            _pauseHandler = pauseHandler;
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            _pauseHandler.Add(this);
            Move();
        }

        private void OnDisable()
        {
            _pauseHandler.Remove(this);
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
                gameObject.SetActive(false);
                Destroyed?.Invoke(this);
            }
        }

        private void Move()
        {
            _rigidbody.AddForce(transform.up * _bulletSpeed);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent<IDamageable>(out IDamageable obstacle))
            {
                ObstacleHit?.Invoke(obstacle);
                obstacle.TakeHit(HitType.Missile);
            }

            gameObject.SetActive(false);
            Destroyed?.Invoke(this);
        }

        public void Pause()
        {
            _linearVelocity = _rigidbody.linearVelocity;
            _rigidbody.bodyType = RigidbodyType2D.Static;
        }

        public void Unpause()
        {
            _rigidbody.bodyType = RigidbodyType2D.Dynamic;
            _rigidbody.linearVelocity = _linearVelocity;
        }
    }
}
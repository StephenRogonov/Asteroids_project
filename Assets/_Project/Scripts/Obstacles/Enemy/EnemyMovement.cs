using _Project.Scripts.GameFlow;
using _Project.Scripts.Obstacles;
using _Project.Scripts.Player;
using _Project.Scripts.PlayerWeapons;
using System;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Enemy
{
    public class EnemyMovement : MonoBehaviour, IDamageable, IPause
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _rotationSpeed;

        private Rigidbody2D _rigidbody;
        private Transform _player;
        private Vector2 _playerDirection;
        private ObstacleType _obstacleType;
        private PauseHandler _pauseHandler;

        private bool _isPaused;

        public ObstacleType ObstacleType => _obstacleType;

        public event Action<EnemyMovement> Destroyed;

        [Inject]
        private void Construct(ShipCollision shipCollision, PauseHandler pauseHandler)
        {
            _player = shipCollision.transform;
            _pauseHandler = pauseHandler;
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _obstacleType = ObstacleType.Enemy;
        }

        private void OnEnable()
        {
            _pauseHandler.Add(this);
        }

        private void OnDisable()
        {
            _pauseHandler.Remove(this);
        }

        private void Update()
        {
            GetPlayerDirection();
        }

        private void FixedUpdate()
        {
            RotateTowardsPlayer();

            if (_isPaused == false)
            {
                SetVelocity();
            }
        }

        private void GetPlayerDirection()
        {
            _playerDirection = (_player.position - transform.position).normalized;
        }

        private void RotateTowardsPlayer()
        {
            if (_playerDirection == Vector2.zero)
            {
                return;
            }

            Quaternion playerRotation = Quaternion.LookRotation(transform.forward, _playerDirection);
            Quaternion rotation = Quaternion.RotateTowards(transform.rotation, playerRotation, _rotationSpeed * Time.deltaTime);

            _rigidbody.SetRotation(rotation);
        }

        private void SetVelocity()
        {
            if (_playerDirection == Vector2.zero)
            {
                _rigidbody.linearVelocity = Vector2.zero;
            }
            else
            {
                _rigidbody.linearVelocity = transform.up * _speed;
            }
        }

        public void TakeHit(WeaponType hitType)
        {
            DestroyObject();
        }

        public void DestroyObject()
        {
            gameObject.SetActive(false);
            Destroyed?.Invoke(this);
        }

        public void Pause()
        {
            _isPaused = true;
            _rigidbody.bodyType = RigidbodyType2D.Static;
        }

        public void Unpause()
        {
            _rigidbody.bodyType = RigidbodyType2D.Dynamic;
            _isPaused = false;
        }
    }
}
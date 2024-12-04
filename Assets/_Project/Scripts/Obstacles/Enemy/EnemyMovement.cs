using _Project.Scripts.Obstacles;
using _Project.Scripts.PlayerWeapons;
using _Project.Scripts.Player;
using System;
using UnityEngine;

namespace _Project.Scripts.Enemy
{
    public class EnemyMovement : MonoBehaviour, IDamageable
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _rotationSpeed;

        private Rigidbody2D _rigidbody;
        private Transform _player;
        private Vector2 _playerDirection;
        private ObstacleType _obstacleType;

        public ObstacleType ObstacleType => _obstacleType;

        public event Action<EnemyMovement> Destroyed;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _obstacleType = ObstacleType.Enemy;
        }

        private void Update()
        {
            GetPlayerDirection();
        }

        private void FixedUpdate()
        {
            RotateTowardsPlayer();
            SetVelocity();
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

        public void SetPlayerTransform(ShipCollision shipCollision)
        {
            _player = shipCollision.transform;
        }

        public void TakeHit(WeaponType hitType)
        {
            gameObject.SetActive(false);
            Destroyed?.Invoke(this);
        }
    }
}
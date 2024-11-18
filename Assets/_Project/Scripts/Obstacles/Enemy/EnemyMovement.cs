using _Project.Scripts.Obstacles;
using _Project.Scripts.PlayerWeapons;
using System;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Enemy
{
    public class EnemyMovement : MonoBehaviour, IDamageable
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _rotationSpeed;

        private IEnemyTarget _shipToFollow;
        private Rigidbody2D _rigidbody;
        //private Transform _player;
        private Vector2 _playerDirection;

        public event Action<EnemyMovement> Destroyed;

        [Inject]
        private void Construct(IEnemyTarget target)
        {
            _shipToFollow = target;
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
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
            _playerDirection = (_shipToFollow.Position - transform.position).normalized;
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

        //public void Setup(ShipCollision shipCollision)
        //{
        //    _player = shipCollision.transform;
        //}

        public void TakeHit(WeaponType hitType)
        {
            gameObject.SetActive(false);
            Destroyed?.Invoke(this);
        }
    }
}
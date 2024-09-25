using _Project.Scripts.Common;
using _Project.Scripts.Obstacles;
using _Project.Scripts.Player;
using _Project.Scripts.PlayerWeapons;
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
        private Pool<EnemyMovement> _pool;

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
                _rigidbody.velocity = Vector2.zero;
            }
            else
            {
                _rigidbody.velocity = transform.up * _speed;
            }
        }

        public void SetPool(Pool<EnemyMovement> pool)
        {
            _pool = pool;
        }

        public void Setup(ShipMovement shipMovement)
        {
            _player = shipMovement.transform;
        }

        public void TakeHit(WeaponType hitType)
        {
            gameObject.SetActive(false);
            _pool.Return(this);
        }
    }
}
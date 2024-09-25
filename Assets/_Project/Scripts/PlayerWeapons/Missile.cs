using _Project.Scripts.Common;
using _Project.Scripts.Obstacles;
using UnityEngine;

namespace _Project.Scripts.PlayerWeapons
{
    public class Missile : MonoBehaviour
    {
        [SerializeField] private float _bulletSpeed = 500f;

        private Rigidbody2D _rigidbody;
        private Pool<Missile> _pool; 
        private Vector2 _screenPosition;
        private Camera _mainCamera;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _mainCamera = Camera.main;
        }

        private void OnEnable()
        {
            Move();
        }

        public void SetPool(Pool<Missile> pool)
        {
            _pool = pool;
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
                _pool.Return(this);
            }
        }

        private void Move()
        {
            _rigidbody.AddForce(transform.up * _bulletSpeed);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.GetComponent<IDamageable>() != null)
            {
                collision.gameObject.GetComponent<IDamageable>().TakeHit(WeaponType.Missile);
            }

            gameObject.SetActive(false);
            _pool.Return(this);
        }
    }
}
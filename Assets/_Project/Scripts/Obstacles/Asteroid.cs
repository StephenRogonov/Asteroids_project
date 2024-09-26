using _Project.Scripts.Common;
using _Project.Scripts.PlayerWeapons;
using System.Collections;
using UnityEngine;

namespace _Project.Scripts.Obstacles
{
    public class Asteroid : MonoBehaviour, IDamageable
    {
        [SerializeField] private float _speed = 5f;
        [SerializeField] private float _destroyTimeout = 30f;
        [SerializeField] private float _shardSize = 0.75f;
        [SerializeField] private int _shardsAmount = 2;

        private Rigidbody2D _rigidbody;
        private Pool<Asteroid> _pool;
        private AsteroidType _type;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            Move(transform.up);
            StartCoroutine(DisableObject());
        }

        private IEnumerator DisableObject()
        {
            yield return new WaitForSeconds(_destroyTimeout);

            DestroyObject();
        }

        public void SetPool(Pool<Asteroid> pool)
        {
            _pool = pool;
        }

        public void SetType(AsteroidType type)
        {
            _type = type;
        }

        public void Move(Vector2 direction)
        {
            _rigidbody.AddForce(direction * _speed);
        }

        public void TakeHit(WeaponType hitType)
        {
            if (hitType == WeaponType.Missile && _type == AsteroidType.Asteroid)
            {
                for (int i = 0; i < _shardsAmount; i++)
                {
                    CreateShard();
                }
            }

            DestroyObject();
        }

        private void CreateShard()
        {
            Vector2 position = transform.position;
            position += Random.insideUnitCircle * 0.5f;

            Asteroid shard = Instantiate(this, position, transform.rotation);
            shard.Move(Random.insideUnitCircle.normalized);
            shard.SetType(AsteroidType.Shard);
            shard.transform.localScale = new Vector2(_shardSize, _shardSize);
        }

        private void DestroyObject()
        {
            if (_type == AsteroidType.Shard)
            {
                Destroy(gameObject);
            }
            else if (_type == AsteroidType.Asteroid)
            {
                gameObject.SetActive(false);
                _pool.Return(this);
            }
        }
    }
}
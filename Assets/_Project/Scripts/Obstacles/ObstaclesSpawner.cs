using _Project.Scripts.Common;
using _Project.Scripts.Enemy;
using _Project.Scripts.Player;
using System.Collections;
using UnityEngine;

namespace _Project.Scripts.Obstacles
{
    public class ObstaclesSpawner : MonoBehaviour
    {
        [Header("Asteroids Settings")]
        [SerializeField] private Asteroid _asteroidPrefab;
        [SerializeField] private int _asteroidsPoolInitialSize = 10;
        [SerializeField] private float _asteroidSpawnRate = 2f;
        [SerializeField] private float _asteroidAngleOffset = 20f;

        [Header("Enemies Settings")]
        [SerializeField] private EnemyMovement _enemyPrefab;
        [SerializeField] private int _enemiesPoolInitialSize = 4;
        [SerializeField] private float _enemySpawnRate = 10f;

        [Header("Spawn Settings")]
        [SerializeField] private float _spawnDistance = 13f;
        [SerializeField] private ShipMovement _player;

        private Pool<Asteroid> _asteroidsPool;
        private Pool<EnemyMovement> _enemiesPool;

        private void Awake()
        {
            _asteroidsPool = new Pool<Asteroid>(_asteroidPrefab, _asteroidsPoolInitialSize);
            _enemiesPool = new Pool<EnemyMovement>(_enemyPrefab, _enemiesPoolInitialSize);
        }

        private void OnEnable()
        {
            StartCoroutine(AsteroidsSpawning());
            StartCoroutine(EnemySpawning());
        }

        private void OnDisable()
        {
            StopCoroutine(AsteroidsSpawning());
            StopCoroutine(EnemySpawning());
        }

        private IEnumerator AsteroidsSpawning()
        {
            while (true)
            {
                yield return new WaitForSeconds(_asteroidSpawnRate);

                Vector3 spawnOffset = GetRandomSpawnPosition();
                float upDirectionAngleOffset = Random.Range(-_asteroidAngleOffset, _asteroidAngleOffset);
                Quaternion directionOffset = Quaternion.AngleAxis(upDirectionAngleOffset, Vector3.forward);
                Quaternion rotation = Quaternion.LookRotation(transform.forward, -spawnOffset) * directionOffset;

                Asteroid asteroid = _asteroidsPool.Get();
                asteroid.transform.position = spawnOffset;
                asteroid.transform.rotation = rotation;
                asteroid.SetPool(_asteroidsPool);
                asteroid.SetType(AsteroidType.Asteroid);
                asteroid.gameObject.SetActive(true);
            }
        }

        private IEnumerator EnemySpawning()
        {
            while (true)
            {
                yield return new WaitForSeconds(_enemySpawnRate);

                Vector3 spawnOffset = GetRandomSpawnPosition();

                EnemyMovement enemy = _enemiesPool.Get();
                enemy.transform.position = spawnOffset;
                enemy.SetPool(_enemiesPool);
                enemy.Setup(_player);
                enemy.gameObject.SetActive(true);
            }
        }

        private Vector3 GetRandomSpawnPosition()
        {
            return Random.insideUnitCircle.normalized * _spawnDistance;
        }
    }
}
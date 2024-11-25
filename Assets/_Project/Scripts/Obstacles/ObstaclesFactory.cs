using _Project.Scripts.Common;
using _Project.Scripts.Enemy;
using _Project.Scripts.Player;
using UnityEngine;

namespace _Project.Scripts.Obstacles
{
    public class ObstaclesFactory
    {
        private ShipCollision _ship;
        private ObstacleSpawnerSettings _spawnerSettings;
        private Pool<Asteroid> _asteroidsPool;
        private Pool<EnemyMovement> _enemiesPool;

        public ObstaclesFactory(ShipCollision ship, ObstacleSpawnerSettings settings)
        {
            _ship = ship;
            _spawnerSettings = settings;

            CreatePools();
        }

        public void CreatePools()
        {
            _asteroidsPool = new Pool<Asteroid>(_spawnerSettings.AsteroidPrefab, _spawnerSettings.AsteroidsPoolInitialSize);
            _enemiesPool = new Pool<EnemyMovement>(_spawnerSettings.EnemyPrefab, _spawnerSettings.EnemiesPoolInitialSize);
        }

        public Asteroid GetAsteroid()
        {
            Asteroid asteroid = _asteroidsPool.Get();
            asteroid.Destroyed -= _asteroidsPool.Return;
            asteroid.Destroyed += _asteroidsPool.Return;

            Vector3 spawnOffset = GetRandomSpawnPosition();
            float upDirectionAngleOffset = Random.Range(-_spawnerSettings.AsteroidAngleOffset, _spawnerSettings.AsteroidAngleOffset);
            Quaternion directionOffset = Quaternion.AngleAxis(upDirectionAngleOffset, Vector3.forward);
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, -spawnOffset) * directionOffset;

            asteroid.transform.position = spawnOffset;
            asteroid.transform.rotation = rotation;
            asteroid.SetType(AsteroidType.Asteroid);
            asteroid.gameObject.SetActive(true);

            return asteroid;
        }

        public EnemyMovement GetEnemy()
        {
            EnemyMovement enemy = _enemiesPool.Get();
            enemy.Destroyed -= _enemiesPool.Return;
            enemy.Destroyed += _enemiesPool.Return;

            Vector3 spawnOffset = GetRandomSpawnPosition();
            enemy.transform.position = spawnOffset;
            enemy.SetPlayerTransform(_ship);
            enemy.gameObject.SetActive(true);

            return enemy;
        }

        private Vector3 GetRandomSpawnPosition()
        {
            return Random.insideUnitCircle.normalized * _spawnerSettings.SpawnDistance;
        }
    }
}
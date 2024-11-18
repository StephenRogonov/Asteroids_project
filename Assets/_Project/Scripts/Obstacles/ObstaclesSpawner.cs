using System.Collections;
using UnityEngine;

namespace _Project.Scripts.Obstacles
{
    public class ObstaclesSpawner : MonoBehaviour
    {
        private ObstacleSpawnerSettings _settings;
        private ObstaclesFactory _obstaclesFactory;
        private Coroutine _asteroidsSpawnCoroutine;
        private Coroutine _enemiesSpawnCoroutine;

        public void Run(ObstacleSpawnerSettings settings, ObstaclesFactory obstaclesFactory)
        {
            _obstaclesFactory = obstaclesFactory;
            _settings = settings;

            _asteroidsSpawnCoroutine =  StartCoroutine(AsteroidsSpawning());
            _enemiesSpawnCoroutine = StartCoroutine(EnemySpawning());
        }

        public void Stop()
        {
            StopCoroutine(_asteroidsSpawnCoroutine);
            StopCoroutine(_enemiesSpawnCoroutine);
        }

        private IEnumerator AsteroidsSpawning()
        {
            while (true)
            {
                yield return new WaitForSeconds(_settings.AsteroidSpawnRate);
                _obstaclesFactory.GetAsteroid();
            }
        }

        private IEnumerator EnemySpawning()
        {
            while (true)
            {
                yield return new WaitForSeconds(_settings.EnemySpawnRate);
                _obstaclesFactory.GetEnemy();
            }
        }
    }
}
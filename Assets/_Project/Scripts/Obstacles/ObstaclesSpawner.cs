using _Project.Scripts.Bootstrap.Configs;
using _Project.Scripts.GameFlow;
using System.Collections;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Obstacles
{
    public class ObstaclesSpawner : MonoBehaviour, IPause
    {
        private ObstacleSpawnerSettings _settings;
        private GameConfig _remoteConfig;
        private ObstaclesFactory _obstaclesFactory;
        private PauseHandler _pauseHandler;

        private Coroutine _asteroidsSpawnCoroutine;
        private Coroutine _enemiesSpawnCoroutine;

        private bool _isPaused;

        [Inject]
        private void Construct(ObstacleSpawnerSettings settings, GameConfig remoteConfig, ObstaclesFactory obstaclesFactory, PauseHandler pauseHandler)
        {
            _settings = settings;
            _remoteConfig = remoteConfig;
            _obstaclesFactory = obstaclesFactory;
            _pauseHandler = pauseHandler;

            _pauseHandler.Add(this);

            _asteroidsSpawnCoroutine = StartCoroutine(AsteroidsSpawning());
            _enemiesSpawnCoroutine = StartCoroutine(EnemySpawning());
        }

        public void Pause()
        {
            _isPaused = true;
        }

        public void Unpause()
        {
            _isPaused = false;
        }

        private IEnumerator AsteroidsSpawning()
        {
            float time = 0;

            while (true)
            {
                while (time < _remoteConfig.AsteroidsSpawnRate)
                {
                    if (_isPaused == false)
                    {
                        time += Time.deltaTime;
                    }
                    
                    yield return null;
                }

                _obstaclesFactory.GetAsteroid();
                time = 0;
            }
        }

        private IEnumerator EnemySpawning()
        {
            float time = 0;

            while (true)
            {
                while (time < _remoteConfig.EnemiesSpawnRate)
                {
                    if (_isPaused == false)
                    {
                        time += Time.deltaTime;
                    }

                    yield return null;
                }

                _obstaclesFactory.GetEnemy();
                time = 0;
            }
        }
    }
}
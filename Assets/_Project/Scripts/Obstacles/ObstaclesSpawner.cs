using _Project.Scripts.Bootstrap.Configs;
using _Project.Scripts.DataPersistence;
using _Project.Scripts.GameFlow;
using System.Collections;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Obstacles
{
    public class ObstaclesSpawner : MonoBehaviour, IPause
    {
        private GameConfig _gameConfig;
        private ObstaclesFactory _obstaclesFactory;
        private PauseHandler _pauseHandler;

        private bool _isPaused;

        [Inject]
        private void Construct(DataPersistenceHandler dataPersistenceHandler, ObstaclesFactory obstaclesFactory, PauseHandler pauseHandler)
        {
            _gameConfig = dataPersistenceHandler.GameConfig;
            _obstaclesFactory = obstaclesFactory;
            _pauseHandler = pauseHandler;

            _pauseHandler.Add(this);

            StartCoroutine(AsteroidsSpawning());
            StartCoroutine(EnemySpawning());
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
                while (time < _gameConfig.AsteroidsSpawnRate)
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
                while (time < _gameConfig.EnemiesSpawnRate)
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
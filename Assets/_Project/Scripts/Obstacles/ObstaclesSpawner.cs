using _Project.Scripts.Bootstrap.Configs;
using _Project.Scripts.DataPersistence;
using _Project.Scripts.GameFlow;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Obstacles
{
    public class ObstaclesSpawner : MonoBehaviour, IPause, IInitializable
    {
        private GameConfig _gameConfig;
        private ObstaclesFactory _obstaclesFactory;
        private PauseSwitcher _pauseHandler;

        private bool _isPaused;

        [Inject]
        private void Construct(DataPersistenceHandler dataPersistenceHandler, ObstaclesFactory obstaclesFactory, PauseSwitcher pauseHandler)
        {
            _gameConfig = dataPersistenceHandler.GameConfig;
            _obstaclesFactory = obstaclesFactory;
            _pauseHandler = pauseHandler;
        }

        public void Initialize()
        {
            _pauseHandler.Add(this);
            StartSpawning();
        }

        private void StartSpawning()
        {
            AsteroidsSpawningStart();
            EnemiesSpawningStart();
        }

        public void Pause()
        {
            _isPaused = true;
        }

        public void Unpause()
        {
            _isPaused = false;
        }

        private async UniTask AsteroidsSpawningStart()
        {
            int delay = (int)_gameConfig.AsteroidsSpawnRate * 1000;

            while (true)
            {
                await UniTask.Delay(delay);

                if (_isPaused == false)
                {
                    _obstaclesFactory.GetAsteroid();
                }
            }
        }

        private async UniTask EnemiesSpawningStart()
        {
            int delay = (int)_gameConfig.EnemiesSpawnRate * 1000;

            while (true)
            {
                await UniTask.Delay(delay);

                if (_isPaused == false)
                {
                    _obstaclesFactory.GetEnemy();
                }
            }
        }
    }
}
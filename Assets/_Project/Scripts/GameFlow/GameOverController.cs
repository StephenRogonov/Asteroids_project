using _Project.Scripts.Obstacles;
using System;

namespace _Project.Scripts.GameFlow
{
    public class GameOverController : IDisposable
    {
        private ShipCollision _shipCollision;
        private ObstaclesSpawner _obstaclesSpawner;
        private GameOver _gameOverPanel;

        public GameOverController(ShipCollision shipCollision, ObstaclesSpawner obstaclesSpawner, GameOver gameOverPanel)
        {
            _shipCollision = shipCollision;
            _obstaclesSpawner = obstaclesSpawner;
            _gameOverPanel = gameOverPanel;
        }

        public void Init()
        {
            _shipCollision.Crashed += GameOver;
        }

        public void Dispose()
        {
            _shipCollision.Crashed -= GameOver;
        }

        private void GameOver()
        {
            _obstaclesSpawner.Stop();
            _gameOverPanel.Enable();
        }
    }
}
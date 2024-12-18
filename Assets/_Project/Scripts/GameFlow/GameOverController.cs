using _Project.Scripts.Common;
using _Project.Scripts.Obstacles;
using _Project.Scripts.Player;
using System;

namespace _Project.Scripts.GameFlow
{
    public class GameOverController : IDisposable
    {
        private ShipCollision _shipCollision;
        private ObstaclesSpawner _obstaclesSpawner;
        private GameOver _gameOverPanel;
        private PauseHandler _pauseHandler;

        public GameOverController(ShipCollision shipCollision, ObstaclesSpawner obstaclesSpawner, GameOver gameOverPanel, PauseHandler pauseHandler)
        {
            _shipCollision = shipCollision;
            _obstaclesSpawner = obstaclesSpawner;
            _gameOverPanel = gameOverPanel;
            _pauseHandler = pauseHandler;

            _shipCollision.Crashed += GameOver;
        }

        public void Dispose()
        {
            _shipCollision.Crashed -= GameOver;
        }

        private void GameOver()
        {
            //_obstaclesSpawner.Stop();
            _pauseHandler.Pause();
            _gameOverPanel.EnableCanvas();
        }
    }
}
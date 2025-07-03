using _Project.Scripts.Obstacles;
using _Project.Scripts.Player;
using System;
using UnityEngine;

namespace _Project.Scripts.GameFlow
{
    public class GameOverController : IDisposable
    {
        private ShipCollision _shipCollision;
        private ObstaclesSpawner _obstaclesSpawner;
        private GameOverMenu _gameOverPanel;
        private PauseHandler _pauseHandler;
        private CanvasGroup _mobileButtonsCanvasGroup;

        public GameOverController(
            ShipCollision shipCollision, 
            ObstaclesSpawner obstaclesSpawner, 
            GameOverMenu gameOverPanel, 
            PauseHandler pauseHandler,
            MobileButtons mobileButtons)
        {
            _shipCollision = shipCollision;
            _obstaclesSpawner = obstaclesSpawner;
            _gameOverPanel = gameOverPanel;
            _pauseHandler = pauseHandler;

            _mobileButtonsCanvasGroup = mobileButtons.GetComponent<CanvasGroup>();

            _shipCollision.Crashed += GameOver;
        }

        public void Dispose()
        {
            _shipCollision.Crashed -= GameOver;
        }

        private void GameOver()
        {
            _pauseHandler.PauseAll();
            _mobileButtonsCanvasGroup.interactable = false;
            _mobileButtonsCanvasGroup.blocksRaycasts = false;
            _gameOverPanel.EnableCanvas();
        }
    }
}
using _Project.Scripts.Player;
using _Project.Scripts.UI;
using UnityEngine;

namespace _Project.Scripts.GameFlow
{
    public class GameOverController
    {
        private ShipCollision _shipCollision;
        private GameOverMenu _gameOverPanel;
        private PauseHandler _pauseHandler;
        private CanvasGroup _mobileButtonsCanvasGroup;

        public GameOverController(
            ShipCollision shipCollision,
            GameOverMenu gameOverPanel, 
            PauseHandler pauseHandler,
            MobileButtons mobileButtons)
        {
            _shipCollision = shipCollision;
            _gameOverPanel = gameOverPanel;
            _pauseHandler = pauseHandler;

            _mobileButtonsCanvasGroup = mobileButtons.GetComponent<CanvasGroup>();

            _shipCollision.SetGameOverController(this);
        }

        public void GameOver()
        {
            _pauseHandler.PauseAll();
            _mobileButtonsCanvasGroup.interactable = false;
            _mobileButtonsCanvasGroup.blocksRaycasts = false;
            _gameOverPanel.EnableCanvas();
        }
    }
}
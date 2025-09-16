using _Project.Scripts.Player;
using _Project.Scripts.UI;
using System;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.GameFlow
{
    public class GameOverHandler : IInitializable, IDisposable
    {
        private GameOverPresenter _gameOverMenu;
        private PauseHandler _pauseHandler;
        private CanvasGroup _mobileButtonsCanvasGroup;
        private ShipCollision _shipCollision;

        public GameOverHandler(
            GameOverPresenter gameOverMenu, 
            PauseHandler pauseHandler,
            MobileButtons mobileButtons,
            ShipCollision shipCollision
            )
        {
            _gameOverMenu = gameOverMenu;
            _pauseHandler = pauseHandler;
            _shipCollision = shipCollision;

            _mobileButtonsCanvasGroup = mobileButtons.GetComponent<CanvasGroup>();
        }

        private void GameOver()
        {
            _pauseHandler.PauseAll();
            _mobileButtonsCanvasGroup.interactable = false;
            _mobileButtonsCanvasGroup.blocksRaycasts = false;
            _gameOverMenu.EnableView();
        }

        public void Initialize()
        {
            _shipCollision.Crashed += GameOver;
        }

        public void Dispose()
        {
            _shipCollision.Crashed -= GameOver;
        }
    }
}
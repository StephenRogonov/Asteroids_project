using _Project.Scripts.UI;
using UnityEngine;

namespace _Project.Scripts.GameFlow
{
    public class GameOverHandler
    {
        private GameOverPresenter _gameOverMenu;
        private PauseHandler _pauseHandler;
        private CanvasGroup _mobileButtonsCanvasGroup;

        public GameOverHandler(
            GameOverPresenter gameOverMenu, 
            PauseHandler pauseHandler,
            MobileButtons mobileButtons)
        {
            _gameOverMenu = gameOverMenu;
            _pauseHandler = pauseHandler;

            _mobileButtonsCanvasGroup = mobileButtons.GetComponent<CanvasGroup>();
        }

        public void GameOver()
        {
            _pauseHandler.PauseAll();
            _mobileButtonsCanvasGroup.interactable = false;
            _mobileButtonsCanvasGroup.blocksRaycasts = false;
            _gameOverMenu.EnableView();
        }
    }
}
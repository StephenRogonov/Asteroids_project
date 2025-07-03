using _Project.Scripts.GameFlow;
using UnityEngine;

namespace _Project.Scripts.UI
{
    public class PauseController
    {
        private PauseMenu _pauseMenu;
        private PauseHandler _pauseHandler;
        private CanvasGroup _mobileButtonsCanvasGroup;

        public PauseController(PauseMenu pauseMenu, PauseHandler pauseHandler, MobileButtons mobileButtons)
        {
            _pauseMenu = pauseMenu;
            _pauseHandler = pauseHandler;

            _mobileButtonsCanvasGroup = mobileButtons.GetComponent<CanvasGroup>();
        }

        public void PauseGame()
        {
            _pauseHandler.PauseAll();
            _mobileButtonsCanvasGroup.interactable = false;
            _mobileButtonsCanvasGroup.blocksRaycasts = false;
            _pauseMenu.EnableCanvas();
        }
    }
}
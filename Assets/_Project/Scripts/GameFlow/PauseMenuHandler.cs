using _Project.Scripts.GameFlow;
using UnityEngine;

namespace _Project.Scripts.UI
{
    public class PauseMenuHandler
    {
        private PausePresenter _pausePresenter;
        private PauseHandler _pauseHandler;
        private CanvasGroup _mobileButtonsCanvasGroup;

        public PauseMenuHandler(PausePresenter pausePresenter, PauseHandler pauseHandler, MobileButtons mobileButtons)
        {
            _pausePresenter = pausePresenter;
            _pauseHandler = pauseHandler;

            _mobileButtonsCanvasGroup = mobileButtons.GetComponent<CanvasGroup>();
        }

        public void PauseGame()
        {
            _pauseHandler.PauseAll();
            _mobileButtonsCanvasGroup.interactable = false;
            _mobileButtonsCanvasGroup.blocksRaycasts = false;
            _pausePresenter.EnableView();
        }
    }
}
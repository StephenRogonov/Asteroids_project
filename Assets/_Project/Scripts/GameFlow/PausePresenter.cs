using _Project.Scripts.Common;
using _Project.Scripts.UI;
using UnityEngine;

namespace _Project.Scripts.GameFlow
{
    public class PausePresenter
    {
        private PauseHandler _pauseHandler;
        private SceneSwitcher _sceneSwitcher;
        private CanvasGroup _mobileButtonsCanvasGroup;
        private PauseView _view;

        public PausePresenter(PauseHandler pauseHandler, SceneSwitcher sceneSwitcher, MobileButtons mobileButtons)
        {
            _pauseHandler = pauseHandler;
            _sceneSwitcher = sceneSwitcher;
            _mobileButtonsCanvasGroup = mobileButtons.GetComponent<CanvasGroup>();
        }

        public void SetView(PauseView view)
        {
            _view = view;
        }

        public void EnableView()
        {
            _view.EnableObject();
        }

        public void ContinueGame()
        {
            _mobileButtonsCanvasGroup.interactable = true;
            _mobileButtonsCanvasGroup.blocksRaycasts = true;
            _pauseHandler.UnpauseAll();
        }

        public void ExitToMenu()
        {
            _sceneSwitcher.LoadMenu();
        }
    }
}

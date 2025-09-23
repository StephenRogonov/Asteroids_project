using _Project.Scripts.Common;
using _Project.Scripts.GameFlow;
using System;
using UnityEngine;

namespace _Project.Scripts.UI
{
    public class PauseModel
    {
        private PauseHandler _pauseHandler;
        private CanvasGroup _mobileButtonsCanvasGroup;
        private SceneSwitcher _sceneSwitcher;

        public event Action Paused;

        public PauseModel(PauseHandler pauseHandler, MobileButtons mobileButtons, SceneSwitcher sceneSwitcher)
        {
            _pauseHandler = pauseHandler;
            _mobileButtonsCanvasGroup = mobileButtons.GetComponent<CanvasGroup>();
            _sceneSwitcher = sceneSwitcher;
        }

        public void PauseGame()
        {
            _pauseHandler.PauseAll();
            _mobileButtonsCanvasGroup.interactable = false;
            _mobileButtonsCanvasGroup.blocksRaycasts = false;
            Paused?.Invoke();
        }

        public void UnpauseGame()
        {
            _mobileButtonsCanvasGroup.interactable = true;
            _mobileButtonsCanvasGroup.blocksRaycasts = true;
            _pauseHandler.UnpauseAll();
        }

        public void ExitToMainMenu()
        {
            _sceneSwitcher.LoadMenu();
        }
    }
}
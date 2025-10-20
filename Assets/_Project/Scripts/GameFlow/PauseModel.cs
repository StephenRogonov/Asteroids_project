using _Project.Scripts.Common;
using _Project.Scripts.GameFlow;
using System;

namespace _Project.Scripts.UI
{
    public class PauseModel
    {
        private PauseSwitcher _pauseHandler;
        private MobileControls _mobileControls;
        private SceneSwitcher _sceneSwitcher;

        public event Action Paused;

        public PauseModel(PauseSwitcher pauseHandler, SceneSwitcher sceneSwitcher)
        {
            _pauseHandler = pauseHandler;
            _sceneSwitcher = sceneSwitcher;
        }

        public void Init(MobileControls mobileControls)
        {
            _mobileControls = mobileControls;
        }

        public void PauseGame()
        {
            _pauseHandler.PauseAll();
            _mobileControls.BlockButtons();
            Paused?.Invoke();
        }

        public void UnpauseGame()
        {
            _mobileControls.UnblockButtons();
            _pauseHandler.UnpauseAll();
        }

        public void ExitToMainMenu()
        {
            _sceneSwitcher.LoadMenu();
        }
    }
}
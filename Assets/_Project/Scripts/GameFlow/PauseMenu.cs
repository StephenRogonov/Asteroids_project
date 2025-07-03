using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.GameFlow
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _exitButton;

        private PauseHandler _pauseHandler;
        private SceneSwitcher _sceneSwitcher;
        private CanvasGroup _mobileButtonsCanvasGroup;

        [Inject]
        private void Construct(PauseHandler pauseHandler, SceneSwitcher sceneSwitcher, MobileButtons mobileButtons)
        {
            _pauseHandler = pauseHandler;
            _sceneSwitcher = sceneSwitcher;

            _mobileButtonsCanvasGroup = mobileButtons.GetComponent<CanvasGroup>();
        }

        private void OnEnable()
        {
            _continueButton.onClick.AddListener(ContinueGame);
            _continueButton.onClick.AddListener(DisableCanvas);
            _exitButton.onClick.AddListener(ExitToMenu);
            _exitButton.onClick.AddListener(DisableCanvas);
        }

        private void OnDisable()
        {
            _continueButton.onClick.RemoveAllListeners();
            _exitButton.onClick.RemoveAllListeners();
        }

        private void ContinueGame()
        {
            _mobileButtonsCanvasGroup.interactable = true;
            _mobileButtonsCanvasGroup.blocksRaycasts = true;
            _pauseHandler.UnpauseAll();
        }

        private void ExitToMenu()
        {
            _sceneSwitcher.LoadMenu();
        }

        public void EnableCanvas()
        {
            gameObject.SetActive(true);
        }

        private void DisableCanvas()
        {
            gameObject.SetActive(false);
        }
    }
}
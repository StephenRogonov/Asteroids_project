using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.GameFlow
{
    public class PauseView : MonoBehaviour
    {
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _exitButton;

        private PausePresenter _presenter;

        [Inject]
        private void Construct(PausePresenter presenter)
        {
            _presenter = presenter;
            _presenter.SetView(this);
        }

        private void OnEnable()
        {
            _continueButton.onClick.AddListener(Continue);
            _continueButton.onClick.AddListener(DisableObject);
            _exitButton.onClick.AddListener(Exit);
            _exitButton.onClick.AddListener(DisableObject);
        }

        private void OnDisable()
        {
            _continueButton.onClick.RemoveAllListeners();
            _exitButton.onClick.RemoveAllListeners();
        }

        private void Continue()
        {
            _presenter.ContinueGame();
        }

        private void Exit()
        {
            _presenter.ExitToMenu();
        }

        public void EnableObject()
        {
            gameObject.SetActive(true);
        }

        private void DisableObject()
        {
            gameObject.SetActive(false);
        }
    }
}
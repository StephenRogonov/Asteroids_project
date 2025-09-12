using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.GameFlow
{
    public class GameOverView : MonoBehaviour
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _continueButton;

        private GameOverPresenter _presenter;

        [Inject]
        private void Construct(GameOverPresenter presenter)
        {
            _presenter = presenter;
            _presenter.SetView(this);
        }

        private void OnEnable()
        {
            _restartButton.onClick.AddListener(Restart);
            _continueButton.onClick.AddListener(Continue);
            _continueButton.onClick.AddListener(DisableObject);
            _restartButton.onClick.AddListener(DisableObject);
        }

        private void OnDisable()
        {
            _restartButton.onClick.RemoveAllListeners();
            _continueButton.onClick.RemoveAllListeners();
        }

        private void Restart()
        {
            _presenter.RestartGame();
        }

        private void Continue()
        {
            _presenter.Continue();
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

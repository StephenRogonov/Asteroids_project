using _Project.Scripts.Common;
using _Project.Scripts.Obstacles;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.GameFlow
{
    public class GameOver : MonoBehaviour
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _continueButton;

        private Interstitial _interstitial;
        private Rewarded _rewarded;

        private SceneSwitcher _sceneSwitcher;
        private ObstaclesFactory _obstaclesFactory;
        private PauseHandler _pauseHandler;

        [Inject]
        private void Construct(SceneSwitcher sceneSwitcher, ObstaclesFactory obstaclesFactory, PauseHandler pauseHandler)
        {
            _sceneSwitcher = sceneSwitcher;
            _obstaclesFactory = obstaclesFactory;
            _pauseHandler = pauseHandler;
        }

        private void OnEnable()
        {
            _interstitial = GetComponent<Interstitial>();
            _rewarded = GetComponent<Rewarded>();
            _restartButton.onClick.AddListener(_interstitial.ShowAd);
            _restartButton.onClick.AddListener(DisableCanvas);
            _continueButton.onClick.AddListener(_rewarded.ShowAd);
            _continueButton.onClick.AddListener(DisableCanvas);
        }

        private void OnDisable()
        {
            _restartButton.onClick.RemoveAllListeners();
            _continueButton.onClick.RemoveAllListeners();
        }

        public void RestartGame()
        {
            gameObject.SetActive(false);
            _sceneSwitcher.LoadScene(SceneSwitcher.GAME);
        }

        public void ContinueGame()
        {
            _obstaclesFactory.ReturnSpawnedToPool();
            _pauseHandler.Unpause();
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
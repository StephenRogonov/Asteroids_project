using _Project.Scripts.Advertising;
using _Project.Scripts.Menu;
using _Project.Scripts.Obstacles;
using _Project.Scripts.Player;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.GameFlow
{
    [RequireComponent(typeof(IInterstitial))]
    [RequireComponent(typeof(IRewarded))]
    public class GameOverMenu : MonoBehaviour
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _continueButton;

        private IInterstitial _interstitial;
        private IRewarded _rewarded;

        private SceneSwitcher _sceneSwitcher;
        private ObstaclesFactory _obstaclesFactory;
        private ShipMovement _ship;
        private PauseHandler _pauseHandler;
        private CanvasGroup _mobileButtonsCanvasGroup;

        [Inject]
        private void Construct(
            SceneSwitcher sceneSwitcher, 
            ObstaclesFactory obstaclesFactory, 
            ShipMovement shipMovement,
            PauseHandler pauseHandler, 
            MobileButtons mobileButtons)
        {
            _sceneSwitcher = sceneSwitcher;
            _obstaclesFactory = obstaclesFactory;
            _ship = shipMovement;
            _pauseHandler = pauseHandler;

            _mobileButtonsCanvasGroup = mobileButtons.GetComponent<CanvasGroup>();
        }

        private void OnEnable()
        {
            if (PlayerPrefs.GetString(PlayerPrefsKeys.NO_ADS_PURCHASED_KEY) == "no")
            {
                _interstitial = GetComponent<IInterstitial>();
                _restartButton.onClick.AddListener(_interstitial.ShowAd);
            }
            else if (PlayerPrefs.GetString(PlayerPrefsKeys.NO_ADS_PURCHASED_KEY) == "yes")
            {
                _restartButton.onClick.AddListener(RestartGame);
            }

            _rewarded = GetComponent<IRewarded>();
            _continueButton.onClick.AddListener(_rewarded.ShowAd);
            _continueButton.onClick.AddListener(DisableCanvas);
            _restartButton.onClick.AddListener(DisableCanvas);
        }

        private void OnDisable()
        {
            _restartButton.onClick.RemoveAllListeners();
            _continueButton.onClick.RemoveAllListeners();
        }

        public void RestartGame()
        {
            _sceneSwitcher.LoadGame();
        }

        public void ContinueGame()
        {
            _pauseHandler.UnpauseAll();
            _obstaclesFactory.ReturnSpawnedToPool();
            _ship.gameObject.SetActive(true);
            _mobileButtonsCanvasGroup.interactable = true;
            _mobileButtonsCanvasGroup.blocksRaycasts = true;
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
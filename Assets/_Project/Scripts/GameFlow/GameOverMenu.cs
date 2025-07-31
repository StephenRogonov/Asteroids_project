using _Project.Scripts.Bootstrap.Advertising;
using _Project.Scripts.MainMenu;
using _Project.Scripts.Obstacles;
using _Project.Scripts.Player;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using System;
using _Project.Scripts.DataPersistence;

namespace _Project.Scripts.GameFlow
{
    public class GameOverMenu : MonoBehaviour
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _continueButton;

        private IInterstitial _interstitial;
        private IRewarded _rewarded;

        private SceneSwitcher _sceneSwitcher;
        private PlayerData _playerData;
        private ObstaclesFactory _obstaclesFactory;
        private ShipMovement _ship;
        private PauseHandler _pauseHandler;
        private CanvasGroup _mobileButtonsCanvasGroup;

        private Action OnInterstitialShown;
        private Action OnRewardedShown;

        [Inject]
        private void Construct(
            SceneSwitcher sceneSwitcher,
            DataPersistenceHandler dataPersistenceHandler,
            ObstaclesFactory obstaclesFactory,
            ShipMovement shipMovement,
            PauseHandler pauseHandler,
            MobileButtons mobileButtons,
            IInterstitial interstitial,
            IRewarded rewarded)
        {
            _playerData = dataPersistenceHandler.PlayerData;
            _sceneSwitcher = sceneSwitcher;
            _obstaclesFactory = obstaclesFactory;
            _ship = shipMovement;
            _pauseHandler = pauseHandler;

            _mobileButtonsCanvasGroup = mobileButtons.GetComponent<CanvasGroup>();
            _interstitial = interstitial;
            _rewarded = rewarded;
        }

        private void OnEnable()
        {
            OnRewardedShown += ContinueGame;

            if (_playerData.NoAdsPurchased == false)
            {
                OnInterstitialShown += RestartGame;
                _restartButton.onClick.AddListener(ShowInterstitial);
            }
            else if (_playerData.NoAdsPurchased == true)
            {
                _restartButton.onClick.AddListener(RestartGame);
            }

            _continueButton.onClick.AddListener(ShowRewarded);
            _continueButton.onClick.AddListener(DisableCanvas);
            _restartButton.onClick.AddListener(DisableCanvas);
        }
        private void OnDisable()
        {
            OnInterstitialShown -= RestartGame;
            OnRewardedShown -= ContinueGame;

            _restartButton.onClick.RemoveAllListeners();
            _continueButton.onClick.RemoveAllListeners();
        }

        private void ShowInterstitial()
        {
            _interstitial.ShowAd(OnInterstitialShown);
        }

        private void ShowRewarded()
        {
            _rewarded.ShowAd(OnRewardedShown);
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
using _Project.Scripts.Bootstrap.Advertising;
using _Project.Scripts.Common;
using _Project.Scripts.DataPersistence;
using _Project.Scripts.Obstacles;
using _Project.Scripts.Player;
using _Project.Scripts.UI;
using System;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.GameFlow
{
    public class GameOverModel : IInitializable, IDisposable
    {
        private PauseHandler _pauseHandler;
        private CanvasGroup _mobileButtonsCanvasGroup;
        private ShipCollision _shipCollision;
        private IInterstitial _interstitial;
        private IRewarded _rewarded;
        private PlayerData _playerData;
        private SceneSwitcher _sceneSwitcher;
        private ObstaclesFactory _obstaclesFactory;
        private ShipMovement _ship;

        private Action OnInterstitialShown;
        private Action OnRewardedShown;

        public event Action GameOverTriggered;

        public GameOverModel(
            PauseHandler pauseHandler,
            MobileButtons mobileButtons,
            ShipCollision shipCollision,
            IInterstitial interstitial,
            IRewarded rewarded,
            DataPersistenceHandler dataPersistenceHandler,
            SceneSwitcher sceneSwitcher,
            ObstaclesFactory obstaclesFactory,
            ShipMovement ship
            )
        {
            _pauseHandler = pauseHandler;
            _mobileButtonsCanvasGroup = mobileButtons.GetComponent<CanvasGroup>();
            _shipCollision = shipCollision;
            _interstitial = interstitial;
            _rewarded = rewarded;
            _playerData = dataPersistenceHandler.PlayerData;
            _sceneSwitcher = sceneSwitcher;
            _obstaclesFactory = obstaclesFactory;
            _ship = ship;
        }

        private void ShowInterstitial()
        {
            _interstitial.ShowAd(OnInterstitialShown);
        }

        private void ShowRewarded()
        {
            _rewarded.ShowAd(OnRewardedShown);
        }

        private void GameOverTrigger()
        {
            _pauseHandler.PauseAll();
            _mobileButtonsCanvasGroup.interactable = false;
            _mobileButtonsCanvasGroup.blocksRaycasts = false;
            GameOverTriggered?.Invoke();
        }

        public void Continue()
        {
            OnRewardedShown += ContinueGame;
            ShowRewarded();
        }

        public void ContinueGame()
        {
            _obstaclesFactory.ReturnSpawnedToPool();
            _ship.ActivateObject();
            _mobileButtonsCanvasGroup.interactable = true;
            _mobileButtonsCanvasGroup.blocksRaycasts = true;
            _pauseHandler.UnpauseAll();
        }

        public void RestartGame()
        {
            if (_playerData.NoAdsPurchased == false)
            {
                OnInterstitialShown += ReloadScene;
                ShowInterstitial();
            }
            else if (_playerData.NoAdsPurchased == true)
            {
                ReloadScene();
            }
        }

        private void ReloadScene()
        {
            OnInterstitialShown -= ReloadScene;
            _sceneSwitcher.LoadGame();
        }

        public void Initialize()
        {
            _shipCollision.Crashed += GameOverTrigger;
        }

        public void Dispose()
        {
            OnRewardedShown -= ContinueGame;
            _shipCollision.Crashed -= GameOverTrigger;
        }
    }
}
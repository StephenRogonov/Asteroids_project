using _Project.Scripts.Bootstrap.Advertising;
using _Project.Scripts.Common;
using _Project.Scripts.DataPersistence;
using _Project.Scripts.Obstacles;
using _Project.Scripts.Player;
using _Project.Scripts.UI;
using System;
using UnityEngine;

namespace _Project.Scripts.GameFlow
{
    public class GameOverPresenter : IDisposable
    {
        private IInterstitial _interstitial;
        private IRewarded _rewarded;

        private GameOverView _view;

        private SceneSwitcher _sceneSwitcher;
        private PlayerData _playerData;
        private PauseHandler _pauseHandler;
        private ObstaclesFactory _obstaclesFactory;
        private ShipMovement _ship;
        private CanvasGroup _mobileButtonsCanvasGroup;

        private Action OnInterstitialShown;
        private Action OnRewardedShown;

        public GameOverPresenter(
            SceneSwitcher sceneSwitcher,
            DataPersistenceHandler dataPersistenceHandler,
            PauseHandler pauseHandler,
            ObstaclesFactory obstaclesFactory,
            ShipMovement shipMovement,
            MobileButtons mobileButtons,
            IInterstitial interstitial,
            IRewarded rewarded
            )
        {
            _sceneSwitcher = sceneSwitcher;
            _playerData = dataPersistenceHandler.PlayerData;
            _pauseHandler = pauseHandler;
            _obstaclesFactory = obstaclesFactory;
            _ship = shipMovement;
            _mobileButtonsCanvasGroup = mobileButtons.GetComponent<CanvasGroup>();
            _interstitial = interstitial;
            _rewarded = rewarded;
        }

        public void SetView(GameOverView view)
        {
            _view = view;
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

        public void Continue()
        {
            OnRewardedShown += ContinueGame;
            ShowRewarded();
        }

        private void ContinueGame()
        {
            _pauseHandler.UnpauseAll();
            _obstaclesFactory.ReturnSpawnedToPool();
            _ship.ActivateObject();
            _mobileButtonsCanvasGroup.interactable = true;
            _mobileButtonsCanvasGroup.blocksRaycasts = true;
        }

        public void EnableView()
        {
            _view.EnableObject();
        }

        public void Dispose()
        {
            OnInterstitialShown -= ReloadScene;
            OnRewardedShown -= Continue;
        }
    }
}
using _Project.Scripts.Bootstrap.Advertising;
using _Project.Scripts.Common;
using _Project.Scripts.DataPersistence;
using _Project.Scripts.Obstacles;
using _Project.Scripts.Player;
using _Project.Scripts.UI;
using System;

namespace _Project.Scripts.GameFlow
{
    public class GameOverModel : IDisposable
    {
        private PauseSwitcher _pauseHandler;
        private MobileControls _mobileControls;
        private ShipCollision _shipCollision;
        private IInterstitial _interstitial;
        private IRewarded _rewarded;
        private PlayerData _playerData;
        private SceneSwitcher _sceneSwitcher;
        private ObstaclesFactory _obstaclesFactory;
        private ShipMovement _shipMovement;

        private Action OnInterstitialShown;
        private Action OnRewardedShown;

        public event Action GameOverTriggered;

        public GameOverModel(
            PauseSwitcher pauseHandler,
            IInterstitial interstitial,
            IRewarded rewarded,
            DataPersistenceHandler dataPersistenceHandler,
            SceneSwitcher sceneSwitcher,
            ObstaclesFactory obstaclesFactory
            )
        {
            _pauseHandler = pauseHandler;
            _interstitial = interstitial;
            _rewarded = rewarded;
            _playerData = dataPersistenceHandler.PlayerData;
            _sceneSwitcher = sceneSwitcher;
            _obstaclesFactory = obstaclesFactory;
        }

        public void Init(MobileControls mobileControls, ShipMovement shipMovement, ShipCollision shipCollision)
        {
            _mobileControls = mobileControls;
            _shipMovement = shipMovement;
            _shipCollision = shipCollision;

            _shipCollision.Crashed += GameOverTrigger;
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
            _mobileControls.BlockButtons();
            GameOverTriggered?.Invoke();
        }

        public void Continue()
        {
            OnRewardedShown += ContinueGame;
            ShowRewarded();
        }

        public void ContinueGame()
        {
            _pauseHandler.UnpauseAll();
            _obstaclesFactory.ReturnSpawnedToPool();
            _shipMovement.ActivateObject();
            _mobileControls.UnblockButtons();
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

        public void Dispose()
        {
            OnRewardedShown -= ContinueGame;
            _shipCollision.Crashed -= GameOverTrigger;
        }
    }
}
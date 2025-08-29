using _Project.Scripts.Bootstrap.Advertising;
using _Project.Scripts.Bootstrap.Configs;
using _Project.Scripts.Bootstrap.Firebase;
using _Project.Scripts.DataPersistence;
using _Project.Scripts.Common;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Bootstrap
{
    public class EntryPoint : MonoBehaviour
    {
        private DataPersistenceHandler _dataPersistence;
        private FirebaseSetup _firebaseSetup;
        private FirebaseRemoteConfigFetcher _configFetcher;
        private AdsInitialization _adsInitialization;

        private SceneSwitcher _sceneSwitcher;

        [Inject]
        private void Construct(FirebaseSetup firebaseSetup,
            FirebaseRemoteConfigFetcher remoteConfigFetcher,
            DataPersistenceHandler dataPersistence,
            AdsInitialization adsInitialization,
            SceneSwitcher sceneSwitcher
            )
        {
            _firebaseSetup = firebaseSetup;
            _configFetcher = remoteConfigFetcher;
            _dataPersistence = dataPersistence;
            _adsInitialization = adsInitialization;
            _sceneSwitcher = sceneSwitcher;
        }

        async void Start()
        {
            await UniTask.WhenAll(
                _firebaseSetup.InitializeFirebase(),
                _configFetcher.FetchData()
                );
            await _dataPersistence.LoadPlayerData();
            await _adsInitialization.InitializeAds();

            _sceneSwitcher.LoadMenu();
        }
    }
}
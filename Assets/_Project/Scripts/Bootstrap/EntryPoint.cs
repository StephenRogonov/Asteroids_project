using _Project.Scripts.Bootstrap.Advertising;
using _Project.Scripts.Bootstrap.Configs;
using _Project.Scripts.Bootstrap.Firebase;
using _Project.Scripts.DataPersistence;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class EntryPoint : MonoBehaviour
{
    private FileDataHandler _fileHandler;
    private DataPersistenceHandler _dataPersistence;
    private FirebaseSetup _firebaseSetup;
    private FirebaseRemoteConfigFetcher _configFetcher;
    private AdsInitialization _adsInitialization;

    private SceneSwitcher _sceneSwitcher;

    [Inject]
    private void Construct(FirebaseSetup firebaseSetup,
        FirebaseRemoteConfigFetcher remoteConfigFetcher,
        FileDataHandler fileDataHandler,
        DataPersistenceHandler dataPersistence,
        AdsInitialization adsInitialization,
        SceneSwitcher sceneSwitcher
        )
    {
        _firebaseSetup = firebaseSetup;
        _configFetcher = remoteConfigFetcher;
        _fileHandler = fileDataHandler;
        _dataPersistence = dataPersistence;
        _adsInitialization = adsInitialization;
        _sceneSwitcher = sceneSwitcher;
    }

    async void Start()
    {
        await UniTask.WhenAll(
            _firebaseSetup.InitializeFirebaseUniTask(),
            _configFetcher.FetchDataUniTask()
            );
        await _dataPersistence.LoadGameConfigUniTask();
        await _dataPersistence.LoadPlayerDataUniTask();
        await _adsInitialization.InitializeAdsUniTask();

        _sceneSwitcher.LoadMenu();
    }
}

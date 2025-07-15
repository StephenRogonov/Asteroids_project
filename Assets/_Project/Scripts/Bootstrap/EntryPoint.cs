using _Project.Scripts.Bootstrap.Advertising;
using _Project.Scripts.Bootstrap.Configs;
using _Project.Scripts.Bootstrap.Firebase;
using UnityEngine;
using Zenject;

public class EntryPoint : MonoBehaviour
{
    private FirebaseSetup _firebaseSetup;
    private FirebaseRemoteConfigFetcher _configFetcher;
    private AdsInitialization _adsInitialization;

    private SceneSwitcher _sceneSwitcher;

    [Inject]
    private void Construct(FirebaseSetup firebaseSetup,
        FirebaseRemoteConfigFetcher remoteConfigFetcher,
        AdsInitialization adsInitialization,
        SceneSwitcher sceneSwitcher
        )
    {
        _firebaseSetup = firebaseSetup;
        _configFetcher = remoteConfigFetcher;
        _adsInitialization = adsInitialization;
        _sceneSwitcher = sceneSwitcher;
    }

    async void Start()
    {
        await _firebaseSetup.InitializeFirebase();
        await _configFetcher.FetchDataAsync();
        await _adsInitialization.InitializeAdsAsync();

        _sceneSwitcher.LoadMenu();
    }
}

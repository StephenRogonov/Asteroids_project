using _Project.Scripts.Configs;
using Firebase;
using Firebase.Extensions;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Firebase
{
    public class FirebaseSetup : MonoBehaviour
    {
        private SceneSwitcher _sceneSwitcher;
        private FirebaseRemoteConfigFetcher _configFetcher;

        [Inject]
        private void Construct(SceneSwitcher sceneSwitcher, FirebaseRemoteConfigFetcher configFetcher)
        {
            _sceneSwitcher = sceneSwitcher;
            _configFetcher = configFetcher;
        }

        async void Start()
        {
            await FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(async task =>
            {
                var dependencyStatus = task.Result;
                if (dependencyStatus == DependencyStatus.Available)
                {
                    FirebaseApp app = FirebaseApp.DefaultInstance;
                }
                else
                {
                    Debug.LogError(string.Format(
                      "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                }
            });

            await _configFetcher.FetchDataAsync();
            //_sceneSwitcher.LoadGame();
            _sceneSwitcher.LoadMenu();
        }
    }
}
using Firebase;
using Firebase.Extensions;
using System;
using UnityEngine;
using Zenject;

public class FirebaseSetup : MonoBehaviour
{
    private SceneSwitcher _sceneSwitcher;

    [Inject]
    private void Construct(SceneSwitcher sceneSwitcher)
    {
        _sceneSwitcher = sceneSwitcher;
    }

    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                FirebaseApp app = FirebaseApp.DefaultInstance;
            }
            else
            {
                Debug.LogError(String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
            }

            _sceneSwitcher.LoadScene(SceneSwitcher.GAME);
        });
    }
}

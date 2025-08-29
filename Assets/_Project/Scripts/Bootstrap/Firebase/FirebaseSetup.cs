using Cysharp.Threading.Tasks;
using Firebase;
using Firebase.Extensions;
using UnityEngine;

namespace _Project.Scripts.Bootstrap.Firebase
{
    public class FirebaseSetup
    {
        public async UniTask InitializeFirebase()
        {
            await FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(async task =>
            {
                var dependencyStatus = task.Result;
                if (dependencyStatus == DependencyStatus.Available)
                {
                    Debug.Log("Firebase dependencies successfully resolved.");
                }
                else
                {
                    Debug.LogError(string.Format(
                      "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                }
            });
        }
    }
}
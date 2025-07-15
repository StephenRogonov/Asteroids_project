using Firebase;
using Firebase.Extensions;
using System.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Bootstrap.Firebase
{
    public class FirebaseSetup
    {
        public async Task InitializeFirebase()
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
        }
    }
}
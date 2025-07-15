using Firebase.RemoteConfig;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Bootstrap.Configs
{
    public class FirebaseRemoteConfigFetcher
    {
        private RemoteConfig _configs;

        public FirebaseRemoteConfigFetcher(RemoteConfig configs)
        {
            _configs = configs;
        }

        public async Task FetchDataAsync()
        {
#if UNITY_EDITOR
            Task fetchTask = FirebaseRemoteConfig.DefaultInstance.FetchAsync(TimeSpan.Zero);
#else
            Task fetchTask = FirebaseRemoteConfig.DefaultInstance.FetchAsync(TimeSpan.FromHours(24));
#endif
            await fetchTask;
            await FetchComplete(fetchTask);
        }

        private async Task FetchComplete(Task fetchTask)
        {
            if (fetchTask.IsCompleted == false)
            {
                Debug.LogError("Retrieval hasn't finished.");
                return;
            }

            var remoteConfig = FirebaseRemoteConfig.DefaultInstance;
            var info = remoteConfig.Info;

            if (info.LastFetchStatus != LastFetchStatus.Success)
            {
                Debug.LogError($"{nameof(FetchComplete)} was unsuccessful\n{nameof(info.LastFetchStatus)}: {info.LastFetchStatus}");
                return;
            }

            await remoteConfig.ActivateAsync();
            Debug.Log($"Remote data loaded and ready for use. Last fetch time {info.FetchTime}.");

            _configs.ParseJson();
        }
    }
}
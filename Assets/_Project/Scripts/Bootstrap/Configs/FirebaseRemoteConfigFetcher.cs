using Cysharp.Threading.Tasks;
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

        public async UniTask FetchDataUniTask()
        {
#if UNITY_EDITOR
            await FirebaseRemoteConfig.DefaultInstance.FetchAsync(TimeSpan.Zero).AsUniTask();
#else
            await FirebaseRemoteConfig.DefaultInstance.FetchAsync(TimeSpan.FromHours(24)).AsUniTask();
#endif
            await FetchCompleteUniTask();
        }

        private async UniTask FetchCompleteUniTask()
        {
            var remoteConfig = FirebaseRemoteConfig.DefaultInstance;
            var info = remoteConfig.Info;

            if (info.LastFetchStatus != LastFetchStatus.Success)
            {
                Debug.LogError($"{nameof(FetchCompleteUniTask)} was unsuccessful\n{nameof(info.LastFetchStatus)}: {info.LastFetchStatus}");
                return;
            }

            await remoteConfig.ActivateAsync();
            Debug.Log($"Remote data loaded and ready for use. Last fetch time {info.FetchTime}.");

            _configs.ParseJson();
        }
    }
}
using _Project.Scripts.DataPersistence;
using Cysharp.Threading.Tasks;
using Firebase.RemoteConfig;
using System;
using UnityEngine;

namespace _Project.Scripts.Bootstrap.Configs
{
    public class FirebaseRemoteConfigFetcher
    {
        private FileDataHandler _fileDataHandler;
        private GameConfig _configs;

        public FirebaseRemoteConfigFetcher(GameConfig configs, FileDataHandler fileDataHandler)
        {
            _fileDataHandler = fileDataHandler;
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

            await _fileDataHandler.UpdateGameConfigsUniTask(remoteConfig.GetValue("gameConfigs").StringValue);
        }
    }
}
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _Project.Scripts.AddressablesHandling
{
    public class LocalAssetLoader : ILocalAssetLoader
    {
        private GameObject _cachedObject;

        public async UniTask<T> LoadInternalAsset<T>(string assetID)
        {
            var handle = Addressables.LoadAssetAsync<GameObject>(assetID);
            _cachedObject = await handle.ToUniTask();

            if (_cachedObject.TryGetComponent(out T asset) == false)
            {
                throw new NullReferenceException($"Object of type {typeof(T)} is null on attempt to load it from addressables.");
            }

            return asset;
        }

        public async UniTask<T> InstantiateInternalAsset<T>(string assetID)
        {
            var handle = Addressables.InstantiateAsync(assetID);
            _cachedObject = await handle.ToUniTask();

            if (_cachedObject.TryGetComponent(out T asset) == false)
            {
                throw new NullReferenceException($"Object of type {typeof(T)} is null on attempt to load it from addressables.");
            }

            return asset;
        }

        public void UnloadInternalAsset()
        {
            if (_cachedObject == null)
            {
                return;
            }

            Addressables.Release(_cachedObject);
            _cachedObject = null;
        }
    }
}
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.AddressablesHandling
{
    public interface ILocalAssetLoader
    {
        public UniTask<GameObject> LoadAsset<T>(string assetID);
        public UniTask<T> InstantiateAsset<T>(string asssetID);
        public void UnloadAsset(GameObject asset);
    }
}
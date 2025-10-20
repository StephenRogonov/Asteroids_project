using Cysharp.Threading.Tasks;

namespace _Project.Scripts.AddressablesHandling
{
    public interface ILocalAssetLoader
    {
        public UniTask<T> LoadInternalAsset<T>(string assetID);
        public UniTask<T> InstantiateInternalAsset<T>(string asssetID);
        public void UnloadInternalAsset();
    }
}
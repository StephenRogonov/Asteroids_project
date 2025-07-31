using _Project.Scripts.DataPersistence;
using Cysharp.Threading.Tasks;
using System;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

namespace _Project.Scripts.InAppPurchasing
{
    public class IAPController : IDetailedStoreListener, IDataPersistence, IDisposable
    {
        private PurchasingUI _purchasingUI;
        private DataPersistenceHandler _dataPersistenceHandler;
        private IStoreController _storeController;
        private IExtensionProvider _extensionProvider;
        private Action OnPurchaseCompleted;
        private bool _noAdsPurchased;

        public IAPController(PurchasingUI purchasingUI, DataPersistenceHandler dataPersistenceHandler)
        {
            _purchasingUI = purchasingUI;
            _dataPersistenceHandler = dataPersistenceHandler;
            _dataPersistenceHandler.AddDataObject(this);

            InitializationOptions options = new InitializationOptions()
#if UNITY_EDITOR || DEVELOPMENT_BUILD
                .SetEnvironmentName("test");
#else
                .SetEnvironmentName("production");
#endif

            InitializeServices();
        }

        public void Dispose()
        {
            _dataPersistenceHandler.RemoveDataObject(this);
        }

        private async void InitializeServices()
        {
            await UnityServices.InitializeAsync().AsUniTask();
            ResourceRequest request = Resources.LoadAsync<TextAsset>("IAPProductCatalog");
            request.completed += HandleIAPCatalogLoaded;
        }

        private void HandleIAPCatalogLoaded(AsyncOperation operation)
        {
            ResourceRequest request = operation as ResourceRequest;

            Debug.Log($"Loaded Asset: {request.asset}");
            ProductCatalog catalog = JsonUtility.FromJson<ProductCatalog>((request.asset as TextAsset).text);
            Debug.Log($"Loaded catalog with {catalog.allProducts.Count} items");

            StandardPurchasingModule.Instance().useFakeStoreUIMode = FakeStoreUIMode.StandardUser;
            StandardPurchasingModule.Instance().useFakeStoreAlways = true;

#if UNITY_ANDROID
            ConfigurationBuilder builder = ConfigurationBuilder.Instance(
                StandardPurchasingModule.Instance(AppStore.GooglePlay)
                );
#elif UNITY_IOS
            ConfigurationBuilder builder = ConfigurationBuilder.Instance(
                StandardPurchasingModule.Instance(AppStore.AppleAppStore)
                );
#else
            ConfigurationBuilder builder = ConfigurationBuilder.Instance(
                StandardPurchasingModule.Instance(AppStore.NotSpecified)
                );
#endif

            foreach (ProductCatalogItem item in catalog.allProducts)
            {
                builder.AddProduct(item.id, item.type);
            }

            UnityPurchasing.Initialize(this, builder);
        }

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            _storeController = controller;
            _extensionProvider = extensions;
            SetupUI();
        }

        private void SetupUI()
        {
            Product noAds = _storeController.products.WithID("no_ads");
            _purchasingUI.OnPurchase += HandlePurchase;
            _purchasingUI.Setup(noAds);
        }

        private void HandlePurchase(Product product, Action onPurchaseCompleted)
        {
            OnPurchaseCompleted = onPurchaseCompleted;
            _storeController.InitiatePurchase(product);
        }

        public void OnInitializeFailed(InitializationFailureReason error)
        {
            Debug.LogError($"Error initializing IAP because of {error}");
        }

        public void OnInitializeFailed(InitializationFailureReason error, string message)
        {
            Debug.LogError($"Error initializing IAP because of {error}. {message}.");
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            Debug.LogError($"Failed to purchase {product.definition.id} because of {failureReason}.");
            OnPurchaseCompleted?.Invoke();
            OnPurchaseCompleted = null;
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
        {
            Debug.Log($"Successfully purchased {purchaseEvent.purchasedProduct.definition.id}.");
            OnPurchaseCompleted?.Invoke();
            OnPurchaseCompleted = null;

            _noAdsPurchased = true;
            _dataPersistenceHandler.SavePlayerDataUniTask();

            return PurchaseProcessingResult.Complete;
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
        {
            Debug.LogError($"Unable to purchase product {product.definition.id}. {failureDescription.message}.");
            OnPurchaseCompleted?.Invoke();
            OnPurchaseCompleted = null;
        }

        public void SaveData(PlayerData data)
        {
            data.NoAdsPurchased = _noAdsPurchased;
        }
    }
}
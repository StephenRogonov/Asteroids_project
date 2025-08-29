using _Project.Scripts.DataPersistence;
using _Project.Scripts.InAppPurchasing;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.Menu
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button _noAdsButton;

        private DataPersistenceHandler _dataPersistenceHandler;
        private PlayerData _playerData;
        private ShopItemModel _shopItemModel;
        private GameObject _purchaseNoAdsPopup;

        [Inject]
        private void Construct(
            PurchasingUI noAdsPopup, 
            DataPersistenceHandler dataPersistenceHandler,
            ShopItemModel shopItemModel
            )
        {
            _playerData = dataPersistenceHandler.PlayerData;
            _dataPersistenceHandler = dataPersistenceHandler;
            _shopItemModel = shopItemModel;

            if (_playerData.NoAdsPurchased == true)
            {
                _noAdsButton.gameObject.SetActive(false);
                return;
            }

            _purchaseNoAdsPopup = noAdsPopup.gameObject;
        }

        private void OnEnable()
        {
            _dataPersistenceHandler.PlayerDataChanged += UpdateUI;
            _noAdsButton?.onClick.AddListener(ActivateNoAdsPopup);
        }

        private void OnDisable()
        {
            _dataPersistenceHandler.PlayerDataChanged -= UpdateUI;
            _noAdsButton?.onClick.RemoveAllListeners();
        }

        private void UpdateUI()
        {
            if (_playerData.NoAdsPurchased == true)
            {
                _noAdsButton.gameObject.SetActive(false);
            }
        }

        private void ActivateNoAdsPopup()
        {
            _shopItemModel.SetupProductPurchasePopup(ProductsIDs.NO_ADS);
            _purchaseNoAdsPopup?.SetActive(true);
        }
    }
}
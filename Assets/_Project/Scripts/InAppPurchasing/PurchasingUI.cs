using System;
using TMPro;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

namespace _Project.Scripts.InAppPurchasing
{
    public class PurchasingUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private Button _purchaseButton;
        [SerializeField] private Button _backgroundButton;
        [SerializeField] private GameObject _loadingOverlay;
        private IAPPresenter _iAPpresenter;

        public delegate void PurchaseEvent(Product model, Action onComplete);
        public event PurchaseEvent OnPurchase;

        private Product _product;

        public void Init(IAPPresenter iAPPresenter)
        {
            _iAPpresenter = iAPPresenter;
        }

        private void OnEnable()
        {
            _purchaseButton.onClick.AddListener(Purchase);
            _backgroundButton.onClick.AddListener(ClosePopup);
        }

        private void OnDisable()
        {
            _purchaseButton.onClick.RemoveAllListeners();
            _backgroundButton.onClick.RemoveAllListeners();
        }

        public void EnableObject()
        {
            gameObject.SetActive(true);
        }

        private void ClosePopup()
        {
            gameObject.SetActive(false);
        }

        public void SetupPurchasePopup(Product product)
        {
            _priceText.text = $"{product.metadata.localizedPriceString}";
            _product = product;
        }

        public void Purchase()
        {
            _purchaseButton.enabled = false;
            _loadingOverlay.SetActive(true);
            _iAPpresenter.HandlePurchase(_product, HandlePurchaseComplete);
        }

        private void HandlePurchaseComplete()
        {
            ClosePopup();
            _purchaseButton.enabled = true;
            _loadingOverlay.SetActive(false);
        }
    }
}
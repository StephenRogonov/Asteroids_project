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

        public delegate void PurchaseEvent(Product model, Action onComplete);
        public event PurchaseEvent OnPurchase;

        private Product _productModel;

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

        private void ClosePopup()
        {
            gameObject.SetActive(false);
        }

        public void Setup(Product product)
        {
            _productModel = product;
            _priceText.text = $"{_productModel.metadata.localizedPriceString}";
        }

        public void Purchase()
        {
            _purchaseButton.enabled = false;
            _loadingOverlay.SetActive(true);
            OnPurchase?.Invoke(_productModel, HandlePurchaseComplete);
        }

        private void HandlePurchaseComplete() //bool successful
        {
            ClosePopup();
            _purchaseButton.enabled = true;
            _loadingOverlay.SetActive(false);
        }
    }
}
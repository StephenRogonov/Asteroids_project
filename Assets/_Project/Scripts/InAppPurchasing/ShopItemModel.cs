using UnityEngine;
using UnityEngine.Purchasing;

namespace _Project.Scripts.InAppPurchasing
{
    public class ShopItemModel
    {
        private ProductCollection _products;
        private PurchasingUI _purchasingUI;

        public ShopItemModel(PurchasingUI purchasingUI)
        {
            _purchasingUI = purchasingUI;
            Debug.Log("ShopItemModel constructed");
        }

        public void SetProductsCollection(ProductCollection productCollection)
        {
            _products = productCollection;
        }

        public void SetupProductPurchasePopup(string productID)
        {
            _purchasingUI.SetupPurchasePopup(_products.WithID(productID));
        }
    }
}

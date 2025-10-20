using UnityEngine.Purchasing;

namespace _Project.Scripts.InAppPurchasing
{
    public class ShopItemModel
    {
        private ProductCollection _products;
        private PurchasingUI _purchasingUI;

        public void Init(PurchasingUI purchasingUI)
        {
            _purchasingUI = purchasingUI;
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

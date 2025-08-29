using _Project.Scripts.InAppPurchasing;
using _Project.Scripts.Menu;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class MenuInstaller : MonoInstaller
    {
        [SerializeField] private PurchasingUI _purchaseNoAdsUIPrefab;
        [SerializeField] private MainMenu _mainMenuPrefab;

        public override void InstallBindings()
        {
            Container.Bind<PurchaseApplier>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<IAPPresenter>().AsSingle().Lazy();
            Container.Bind<ShopItemModel>().AsSingle().Lazy();
            Container
                .Bind<PurchasingUI>()
                .FromComponentInNewPrefab(_purchaseNoAdsUIPrefab)
                .AsSingle()
                .NonLazy();
            Container
                .Bind<MainMenu>()
                .FromComponentInNewPrefab(_mainMenuPrefab)
                .AsSingle()
                .NonLazy();
        }
    }
}
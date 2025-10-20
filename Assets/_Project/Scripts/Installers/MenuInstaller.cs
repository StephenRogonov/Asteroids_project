using _Project.Scripts.InAppPurchasing;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class MenuInstaller : MonoInstaller
    {
        [SerializeField] private MainMenuLoader _mainMenuLoader;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PurchaseApplier>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<IAPPresenter>().AsSingle().Lazy();
            Container.Bind<ShopItemModel>().AsSingle().Lazy();
            Container.Bind<MainMenuLoader>().FromInstance(_mainMenuLoader).AsSingle();
        }
    }
}
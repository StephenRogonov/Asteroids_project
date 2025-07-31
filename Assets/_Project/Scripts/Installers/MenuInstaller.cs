using _Project.Scripts.InAppPurchasing;
using _Project.Scripts.MainMenu;
using UnityEngine;
using Zenject;

public class MenuInstaller : MonoInstaller
{
    [SerializeField] private PurchasingUI _purchaseNoAdsUIPrefab;
    [SerializeField] private MainMenu _mainMenuPrefab;

    public override void InstallBindings()
    {
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
        Container.BindInterfacesAndSelfTo<IAPController>().AsSingle().NonLazy();
    }
}
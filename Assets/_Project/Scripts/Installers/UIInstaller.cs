using _Project.Scripts.GameFlow;
using _Project.Scripts.PlayerWeapons;
using _Project.Scripts.UI;
using UnityEngine;
using Zenject;

public class UIInstaller : MonoInstaller
{
    [SerializeField] private HudView _hudPrefab;
    [SerializeField] private GameOver _gameOverPanelPrefab;
    [SerializeField] private MobileButtons _mobileButtonsPrefab;

    public override void InstallBindings()
    {
        Container
            .Bind<HudView>()
            .FromComponentInNewPrefab(_hudPrefab)
            .AsSingle()
            .NonLazy();
        Container
            .Bind<MobileButtons>()
            .FromComponentInNewPrefab(_mobileButtonsPrefab)
            .AsSingle()
            .NonLazy();
        Container
            .Bind<GameOver>()
            .FromComponentInNewPrefab(_gameOverPanelPrefab)
            .AsSingle()
            .NonLazy();

        Container.Bind<HudModel>().AsSingle();
        Container.BindInterfacesAndSelfTo<HudController>().AsSingle();
        //Container.Bind<ITickable>().To<HudController>().AsSingle();
        Container.Bind<WeaponTrigger>().AsSingle().NonLazy();
    }
}
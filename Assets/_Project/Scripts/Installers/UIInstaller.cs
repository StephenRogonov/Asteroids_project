using _Project.Scripts.GameFlow;
using _Project.Scripts.PlayerWeapons;
using _Project.Scripts.UI;
using UnityEngine;
using Zenject;

public class UIInstaller : MonoInstaller
{
    [SerializeField] private HUD_View _hudPrefab;
    [SerializeField] private GameOver _gameOverPanelPrefab;

    public override void InstallBindings()
    {
        Container
            .Bind<HUD_View>()
            .FromComponentInNewPrefab(_hudPrefab)
            .AsSingle()
            .NonLazy();
        Container
            .Bind<GameOver>()
            .FromComponentInNewPrefab(_gameOverPanelPrefab)
            .AsSingle()
            .NonLazy();

        Container.Bind<HUD_DataModel>().AsSingle();
        Container.Bind<HUD_Controller>().AsSingle();
        Container.Bind<WeaponTrigger>().AsSingle();
    }
}
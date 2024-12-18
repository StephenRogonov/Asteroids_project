using _Project.Scripts.Player;
using _Project.Scripts.Analytics;
using UnityEngine;
using Zenject;
using _Project.Scripts.Common;

public class GameplayInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<PauseHandler>().AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerControls>().AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerInput>().AsSingle();
        Container.BindInterfacesAndSelfTo<Camera>().FromInstance(Camera.main).AsSingle();
        Container.BindInterfacesAndSelfTo<AnalyticsEventManager>().AsSingle().NonLazy();
    }
}

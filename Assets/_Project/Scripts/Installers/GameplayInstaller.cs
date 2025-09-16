using _Project.Scripts.Bootstrap.Analytics;
using _Project.Scripts.GameFlow;
using _Project.Scripts.Player;
using _Project.Scripts.UI;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<PauseHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerControls>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerInput>().AsSingle();
            Container.BindInterfacesAndSelfTo<Camera>().FromInstance(Camera.main).AsSingle();
            Container.BindInterfacesAndSelfTo<AnalyticsEventManager>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameOverHandler>().AsSingle().Lazy();
            Container.BindInterfacesAndSelfTo<GameOverPresenter>().AsSingle().NonLazy();
            Container.Bind<PauseMenuHandler>().AsSingle().Lazy();
            Container.Bind<PausePresenter>().AsSingle().NonLazy();
        }
    }
}
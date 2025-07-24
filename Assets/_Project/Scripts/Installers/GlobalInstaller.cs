using _Project.Scripts.Bootstrap.Advertising;
using _Project.Scripts.Bootstrap.Configs;
using _Project.Scripts.DataPersistence;
using Zenject;

public class GlobalInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<SceneSwitcher>().AsSingle().NonLazy();
        Container.Bind<FileDataHandler>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<GameConfig>().AsSingle();
        Container.Bind<DataPersistenceHandler>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<Interstitial>().AsSingle();
        Container.BindInterfacesAndSelfTo<Rewarded>().AsSingle();
    }
}

using _Project.Scripts.Bootstrap.Advertising;
using _Project.Scripts.Bootstrap.Configs;
using Zenject;

public class GlobalInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<SceneSwitcher>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<RemoteConfig>().AsSingle();
        Container.BindInterfacesAndSelfTo<Interstitial>().AsSingle();
        Container.BindInterfacesAndSelfTo<Rewarded>().AsSingle();
    }
}

using _Project.Scripts.Configs;
using Zenject;

public class GlobalInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<SceneSwitcher>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<RemoteConfig>().AsSingle();
        //Container.BindInterfacesAndSelfTo<FirebaseRemoteConfigFetcher>().AsSingle().NonLazy();
    }
}

using _Project.Scripts.Bootstrap.Advertising;
using _Project.Scripts.Bootstrap.Configs;
using _Project.Scripts.Bootstrap.Firebase;
using UnityEngine;
using Zenject;

public class BootstrapInstaller : MonoInstaller
{
    [SerializeField] private EntryPoint _entryPoint;

    public override void InstallBindings()
    {
        Container.Bind<FirebaseSetup>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<FirebaseRemoteConfigFetcher>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<AdsInitialization>().AsSingle().NonLazy();
        Container.Bind<EntryPoint>().FromInstance(_entryPoint).AsSingle();
    }
}
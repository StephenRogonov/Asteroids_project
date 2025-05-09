using _Project.Scripts.Configs;
using _Project.Scripts.Firebase;
using UnityEngine;
using Zenject;

public class BootstrapInstaller : MonoInstaller
{
    [SerializeField] private FirebaseSetup _firebaseSetup;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<FirebaseRemoteConfigFetcher>().AsSingle().NonLazy();
        Container.Bind<FirebaseSetup>().FromInstance(_firebaseSetup).AsSingle();
    }
}
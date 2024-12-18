using UnityEngine;
using Zenject;

public class BootstrapInstaller : MonoInstaller
{
    [SerializeField] private FirebaseSetup _firebaseSetup;

    public override void InstallBindings()
    {
        Container.Bind<FirebaseSetup>().FromInstance(_firebaseSetup).AsSingle();
    }
}
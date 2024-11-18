using _Project.Scripts.GameFlow;
using _Project.Scripts.Player;
using UnityEngine;
using Zenject;

public class GameplayInstaller : MonoInstaller
{
    [SerializeField] private Bootstrap _bootstrap;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<PlayerInput>().AsSingle();
        Container.Bind<Bootstrap>().FromInstance(_bootstrap).AsSingle();
    }
}

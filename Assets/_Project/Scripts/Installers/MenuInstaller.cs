using _Project.Scripts.UI;
using UnityEngine;
using Zenject;

public class MenuInstaller : MonoInstaller
{
    [SerializeField] private MainMenu _mainMenuPrefab;

    public override void InstallBindings()
    {
        Container
            .Bind<MainMenu>()
            .FromComponentInNewPrefab(_mainMenuPrefab)
            .AsSingle()
            .NonLazy();
    }
}
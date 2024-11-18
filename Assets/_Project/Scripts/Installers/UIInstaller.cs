using _Project.Scripts.PlayerWeapons;
using _Project.Scripts.UI;
using UnityEngine;
using Zenject;

public class UIInstaller : MonoInstaller
{
    [SerializeField] private HUD_View _uiView;

    public override void InstallBindings()
    {
        Container.Bind<HUD_View>().FromInstance(_uiView).AsSingle();
        Container.Bind<HUD_DataModel>().AsSingle();
        Container.Bind<HUD_Controller>().AsSingle();
        Container.Bind<WeaponTrigger>().AsSingle();
    }
}
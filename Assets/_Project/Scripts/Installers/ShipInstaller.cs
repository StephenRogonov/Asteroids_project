using _Project.Scripts.Bootstrap.Analytics;
using _Project.Scripts.GameFlow;
using _Project.Scripts.PlayerWeapons;
using _Project.Scripts.ScriptableObjects;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class ShipInstaller : MonoInstaller
    {
        [SerializeField] private ShipLaserConfig _shipShootingLaserConfig;

        public override void InstallBindings()
        {
            Container.Bind<ShipLaserConfig>().FromInstance(_shipShootingLaserConfig).AsSingle();
            Container.Bind<MissilesFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<WeaponTrigger>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<AnalyticsEventManager>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameOverModel>().AsSingle().Lazy();
            Container.BindInterfacesAndSelfTo<GameOverPresenter>().AsSingle().Lazy();
        }
    }
}
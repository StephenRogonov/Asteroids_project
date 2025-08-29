using _Project.Scripts.GameFlow;
using _Project.Scripts.PlayerWeapons;
using _Project.Scripts.UI;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] private HudView _hudPrefab;
        [SerializeField] private MobileButtons _mobileButtonsPrefab;
        [SerializeField] private GameOverMenu _gameOverPanelPrefab;
        [SerializeField] private PauseMenu _pauseMenuPrefab;

        public override void InstallBindings()
        {
            Container
                .Bind<HudView>()
                .FromComponentInNewPrefab(_hudPrefab)
                .AsSingle()
                .NonLazy();
            Container
                .Bind<MobileButtons>()
                .FromComponentInNewPrefab(_mobileButtonsPrefab)
                .AsSingle()
                .NonLazy();
            Container
                .Bind<GameOverMenu>()
                .FromComponentInNewPrefab(_gameOverPanelPrefab)
                .AsSingle()
                .NonLazy();
            Container
                .Bind<PauseMenu>()
                .FromComponentInNewPrefab(_pauseMenuPrefab)
                .AsSingle()
                .NonLazy();

            Container.Bind<HudModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<HudPresenter>().AsSingle();
            Container.Bind<WeaponTrigger>().AsSingle().NonLazy();
            Container.Bind<PauseController>().AsSingle().NonLazy();
        }
    }
}
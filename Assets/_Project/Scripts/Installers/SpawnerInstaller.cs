using _Project.Scripts.GameFlow;
using _Project.Scripts.Obstacles;
using UnityEngine;
using Zenject;

public class SpawnerInstaller : MonoInstaller
{
    [SerializeField] private ObstacleSpawnerSettings _spawnerSettings;
    [SerializeField] private ObstaclesSpawner _spawner;

    public override void InstallBindings()
    {
        Container.Bind<ObstacleSpawnerSettings>().FromInstance(_spawnerSettings).AsSingle();
        Container.Bind<ObstaclesFactory>().AsSingle();
        Container.Bind<ObstaclesSpawner>().FromInstance(_spawner).AsSingle();

        Container.Bind<GameOverController>().AsSingle().NonLazy();
    }
}
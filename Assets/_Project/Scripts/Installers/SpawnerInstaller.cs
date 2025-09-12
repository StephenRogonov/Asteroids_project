using _Project.Scripts.Obstacles;
using _Project.Scripts.ScriptableObjects;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class SpawnerInstaller : MonoInstaller
    {
        [SerializeField] private ObstacleSpawnerSettings _spawnerSettings;
        [SerializeField] private ObstaclesSpawner _spawner;

        public override void InstallBindings()
        {
            Container.Bind<ObstacleSpawnerSettings>().FromInstance(_spawnerSettings).AsSingle();
            Container.Bind<ObstaclesFactory>().AsSingle();
            Container.Bind<ObstaclesSpawner>().FromInstance(_spawner).AsSingle();
        }
    }
}
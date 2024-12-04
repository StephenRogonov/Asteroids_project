using _Project.Scripts.Player;
using _Project.Scripts.PlayerWeapons;
using UnityEngine;
using Zenject;

public class ShipInstaller : MonoInstaller
{
    [SerializeField] private ShipMovement _shipPrefab;
    [SerializeField] private Transform _shipSpawnPoint;
    [SerializeField] private ShipMovementConfig _shipConfig;
    [SerializeField] private ShipMissilesConfig _shipShootingMissilesConfig;
    [SerializeField] private ShipLaserConfig _shipShootingLaserConfig;

    public override void InstallBindings()
    {
        BindConfig();
        BindShipInstance();
    }

    private void BindConfig()
    {
        Container.Bind<ShipMovementConfig>().FromInstance(_shipConfig).AsSingle();
        Container.Bind<ShipMissilesConfig>().FromInstance(_shipShootingMissilesConfig).AsSingle();
        Container.Bind<ShipLaserConfig>().FromInstance(_shipShootingLaserConfig).AsSingle();
    }

    private void BindShipInstance()
    {
        Container
            .BindInterfacesAndSelfTo<ShipMovement>()
            .FromComponentInNewPrefab(_shipPrefab)
            .AsSingle()
            .OnInstantiated<ShipMovement>(OnShipInstantiated)
            .NonLazy();
        Container.Bind<MissilesFactory>().AsSingle();
    }

    private void OnShipInstantiated(InjectContext context, ShipMovement ship)
    {
        ship.transform.position = _shipSpawnPoint.position;

        Container.Bind<ShipMissilesAttack>().FromComponentInHierarchy().AsSingle();
        Container.Bind<ShipLaserAttack>().FromComponentInHierarchy().AsSingle();
        Container.Bind<ShipCollision>().FromComponentInHierarchy().AsSingle();
    }
}
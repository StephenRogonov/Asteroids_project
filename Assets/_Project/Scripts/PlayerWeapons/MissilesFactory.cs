using _Project.Scripts.Common;
using _Project.Scripts.Obstacles;
using _Project.Scripts.Player;
using _Project.Scripts.PlayerWeapons;
using _Project.Scripts.Bootstrap.Analytics;
using UnityEngine;
using Zenject;
using _Project.Scripts.Bootstrap.Configs;

public class MissilesFactory
{
    private AnalyticsEventManager _analyticsEventManager;

    private ShipMissilesConfig _shipMissilesSettings;
    private GameConfig _remoteConfig;

    private Transform _shipShootingPoint;
    private Pool<Missile> _missilesPool;
    private IInstantiator _instantiator;

    public MissilesFactory(AnalyticsEventManager analyticsEventManager, 
        ShipMissilesConfig shipMissilesSettings, 
        GameConfig remoteConfig,
        ShipMovement shipMovement, 
        IInstantiator instantiator)
    {
        _analyticsEventManager = analyticsEventManager;
        _shipMissilesSettings = shipMissilesSettings;
        _remoteConfig = remoteConfig;
        _shipShootingPoint = shipMovement.transform.GetComponentInChildren<ShootingPoint>().transform;
        _instantiator = instantiator;

        _missilesPool = _instantiator.Instantiate<Pool<Missile>>(new object[] 
        { _shipMissilesSettings.MissilePrefab, _remoteConfig.MissilesPoolInitialSize });
    }

    public Missile GetMissile()
    {
        Missile missile = _missilesPool.Get();
        missile.Destroyed -= _missilesPool.Return;
        missile.ObstacleHit -= CountDestroyedObstacles;
        missile.Destroyed += _missilesPool.Return;
        missile.ObstacleHit += CountDestroyedObstacles;

        missile.transform.position = _shipShootingPoint.position;
        missile.transform.rotation = _shipShootingPoint.rotation;
        missile.gameObject.SetActive(true);

        return missile;
    }

    private void CountDestroyedObstacles(IDamageable obstacle)
    {
        if (obstacle.ObstacleType == ObstacleType.Asteroid)
        {
            _analyticsEventManager.IncrementParameter(LogParameters.AsteroidsDestroyedTotal);
        }
        else if (obstacle.ObstacleType == ObstacleType.Enemy)
        {
            _analyticsEventManager.IncrementParameter(LogParameters.EnemiesDestroyedTotal);
        }
    }
}

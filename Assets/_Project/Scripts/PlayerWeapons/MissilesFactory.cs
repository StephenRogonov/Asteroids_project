using _Project.Scripts.Common;
using _Project.Scripts.Obstacles;
using _Project.Scripts.Player;
using _Project.Scripts.PlayerWeapons;
using _Project.Scripts.Analytics;
using UnityEngine;
using Zenject;

public class MissilesFactory
{
    private AnalyticsEventManager _analyticsEventManager;

    private ShipMissilesConfig _shipShootingSettings;
    private Transform _shipShootingPoint;
    private Pool<Missile> _missilesPool;
    private IInstantiator _instantiator;

    public MissilesFactory(AnalyticsEventManager analyticsEventManager, 
        ShipMissilesConfig shipShootingSettings, 
        ShipMovement shipMovement, 
        IInstantiator instantiator)
    {
        _analyticsEventManager = analyticsEventManager;
        _shipShootingSettings = shipShootingSettings;
        _shipShootingPoint = shipMovement.transform.GetComponentInChildren<ShootingPoint>().transform;
        _instantiator = instantiator;

        _missilesPool = _instantiator.Instantiate<Pool<Missile>>(new object[] 
        { _shipShootingSettings.MissilePrefab, _shipShootingSettings.MissilePoolInitialSize });
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

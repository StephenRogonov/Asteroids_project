using _Project.Scripts.Common;
using _Project.Scripts.Obstacles;
using _Project.Scripts.Player;
using _Project.Scripts.PlayerWeapons;
using UnityEngine;

public class MissilesFactory
{
    private ShipMissilesConfig _shipShootingSettings;
    private Transform _shipShootingPoint;
    private Pool<Missile> _missilesPool;

    private int _asteroidsDestroyed;
    private int _enemiesDestroyed;

    public int AsteroidsDestroyed => _asteroidsDestroyed;
    public int EnemiesDestroyed => _enemiesDestroyed;

    public MissilesFactory(ShipMissilesConfig shipShootingSettings, ShipMovement shipMovement)
    {
        _shipShootingSettings = shipShootingSettings;
        _shipShootingPoint = shipMovement.transform.GetComponentInChildren<ShootingPoint>().transform;

        _missilesPool = new Pool<Missile>(_shipShootingSettings.MissilePrefab, _shipShootingSettings.MissilePoolInitialSize);

        _asteroidsDestroyed = 0;
        _enemiesDestroyed = 0;
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
            _asteroidsDestroyed++;
        }
        else if (obstacle.ObstacleType == ObstacleType.Enemy)
        {
            _enemiesDestroyed++;
        }
    }
}

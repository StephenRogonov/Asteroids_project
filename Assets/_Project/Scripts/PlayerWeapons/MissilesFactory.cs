using _Project.Scripts.Common;
using _Project.Scripts.PlayerWeapons;
using UnityEngine;

public class MissilesFactory
{
    private ShipShootingMissilesConfig _shipShootingSettings;
    private Transform _shipShootingPoint;
    private Pool<Missile> _missilesPool;

    public MissilesFactory(ShipShootingMissilesConfig shipShootingSettings, Transform shipShootingPoint)
    {
        _shipShootingSettings = shipShootingSettings;
        _shipShootingPoint = shipShootingPoint;

        _missilesPool = new Pool<Missile>(_shipShootingSettings.MissilePrefab, _shipShootingSettings.MissilePoolInitialSize);
    }

    public Missile GetMissile()
    {
        Missile missile = _missilesPool.Get();
        missile.Destroyed -= _missilesPool.Return;
        missile.Destroyed += _missilesPool.Return;

        missile.transform.position = _shipShootingPoint.position;
        missile.transform.rotation = _shipShootingPoint.rotation;
        missile.gameObject.SetActive(true);

        return missile;
    }
}

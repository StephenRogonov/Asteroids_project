using _Project.Scripts.Player;
using _Project.Scripts.PlayerWeapons;
using Firebase.Analytics;
using System;

public class FirebaseEventManager : IDisposable
{
    private WeaponTrigger _weaponTrigger;
    private MissilesFactory _missilesFactory;
    private ShipLaserAttack _shipLaserAttack;
    private ShipCollision _shipCollision;

    private int _totalAsteroidsDestroyed;
    private int _totalEnemiesDestroyed;

    public FirebaseEventManager(WeaponTrigger weaponTrigger, ShipLaserAttack shipLaserAttack, ShipCollision shipCollision, MissilesFactory missilesFactory)
    {
        _weaponTrigger = weaponTrigger;
        _shipLaserAttack = shipLaserAttack;
        _shipCollision = shipCollision;
        _missilesFactory = missilesFactory;

        _totalAsteroidsDestroyed = 0;
        _totalEnemiesDestroyed = 0;

        _weaponTrigger.LaserShot += LogLaserUsage;
        _shipCollision.Crashed += LogEndGame;

        LogStartGame();
    }

    public void Dispose()
    {
        _weaponTrigger.LaserShot -= LogLaserUsage;
        _shipCollision.Crashed -= LogEndGame;
    }

    private void LogStartGame()
    {
        FirebaseAnalytics.LogEvent("Start_game");
    }

    private void LogLaserUsage()
    {
        FirebaseAnalytics.LogEvent("Laser_used");
    }

    private void LogEndGame()
    {
        _totalAsteroidsDestroyed = _missilesFactory.AsteroidsDestroyed + _shipLaserAttack.AsteroidsDestroyed;
        _totalEnemiesDestroyed = _missilesFactory.EnemiesDestroyed + _shipLaserAttack.EnemiesDestroyed;

        FirebaseAnalytics.LogEvent("End_game", new Parameter[] {
        new Parameter("Missile_total", _weaponTrigger.MissilesShotsTotal),
        new Parameter("Laser_total", _weaponTrigger.LaserShotsTotal),
        new Parameter("Asteroids_destroyed", _totalAsteroidsDestroyed),
        new Parameter("Enemies_destroyed", _totalEnemiesDestroyed)
        });
    }
}

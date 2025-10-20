using _Project.Scripts.Player;
using _Project.Scripts.PlayerWeapons;
using Firebase.Analytics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _Project.Scripts.Bootstrap.Analytics
{
    public class AnalyticsEventManager : IAnalyticsEvents, IDisposable
    {
        private ShipCollision _shipCollision;
        private ShipMovement _shipMovement;
        private WeaponTrigger _weaponTrigger;
        private ShipLaserAttack _shipLaserAttack;
        private MissilesFactory _missilesFactory;

        private int _totalMissilesShot;
        private int _totalLaserShot;
        private int _totalAsteroidsDestroyed;
        private int _totalEnemiesDestroyed;

        public AnalyticsEventManager(
            WeaponTrigger weaponTrigger,
            MissilesFactory missilesFactory
            )
        {
            _weaponTrigger = weaponTrigger;
            _missilesFactory = missilesFactory;

            _totalMissilesShot = 0;
            _totalLaserShot = 0;
            _totalAsteroidsDestroyed = 0;
            _totalEnemiesDestroyed = 0;
        }

        public void Init(ShipMovement shipMovement, ShipLaserAttack shipLaserAttack, ShipCollision shipCollision)
        {
            _shipMovement = shipMovement;
            _shipLaserAttack = shipLaserAttack;
            _shipCollision = shipCollision;

            SubscribeToAnalyticsEvents();
        }

        public void LogEventWithoutParameters(string eventName)
        {
            FirebaseAnalytics.LogEvent(eventName);
        }

        public void LogEventWithParameters(string eventName, Dictionary<string, int> parameters)
        {
            Parameter[] pars = new Parameter[parameters.Count];

            for (int i = 0; i < parameters.Count; i++)
            {
                var item = parameters.ElementAt(i);
                pars[i] = new Parameter(item.Key, item.Value);
            }

            FirebaseAnalytics.LogEvent(eventName, pars);
        }

        public void LogEndGame()
        {
            Dictionary<string, int> parameters = new Dictionary<string, int>()
            {
                { LoggingEvents.MISSILES_TOTAL, _totalMissilesShot },
                { LoggingEvents.LASER_TOTAL, _totalLaserShot },
                { LoggingEvents.ASTEROIDS_DESTROYED, _totalAsteroidsDestroyed },
                { LoggingEvents.ENEMIES_DESTROYED, _totalEnemiesDestroyed }
            };

            LogEventWithParameters(LoggingEvents.END_GAME, parameters);
        }

        public void SubscribeToAnalyticsEvents()
        {
            _shipCollision.Crashed += LogEndGame;
            _shipMovement.GameStarted += LogStartGame;
            _weaponTrigger.MissileShot += ShipMissileShot;
            _weaponTrigger.LaserShot += ShipLaserShot;
            _shipLaserAttack.AsteroidDestroyed += AsteroidDestroyed;
            _shipLaserAttack.EnemyDestroyed += EnemyDestroyed;
            _missilesFactory.AsteroidDestroyed += AsteroidDestroyed;
            _missilesFactory.EnemyDestroyed += EnemyDestroyed;
        }

        public void Dispose()
        {
            _shipCollision.Crashed -= LogEndGame;
            _shipMovement.GameStarted -= LogStartGame;
            _weaponTrigger.MissileShot -= ShipMissileShot;
            _weaponTrigger.LaserShot -= ShipLaserShot;
            _shipLaserAttack.AsteroidDestroyed -= AsteroidDestroyed;
            _shipLaserAttack.EnemyDestroyed -= EnemyDestroyed;
            _missilesFactory.AsteroidDestroyed -= AsteroidDestroyed;
            _missilesFactory.EnemyDestroyed -= EnemyDestroyed;
        }

        private void LogStartGame()
        {
            LogEventWithoutParameters(LoggingEvents.START_GAME);
        }

        private void ShipMissileShot()
        {
            _totalMissilesShot++;
        }

        private void ShipLaserShot()
        {
            _totalLaserShot++;
            LogEventWithoutParameters(LoggingEvents.LASER_SHOT);
        }

        private void AsteroidDestroyed()
        {
            _totalAsteroidsDestroyed++;
        }

        private void EnemyDestroyed()
        {
            _totalEnemiesDestroyed++;
        }
    }
}
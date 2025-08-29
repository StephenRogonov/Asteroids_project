using _Project.Scripts.Bootstrap.Analytics;
using _Project.Scripts.Bootstrap.Configs;
using _Project.Scripts.Common;
using _Project.Scripts.DataPersistence;
using _Project.Scripts.Obstacles;
using _Project.Scripts.Player;
using _Project.Scripts.ScriptableObjects;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.PlayerWeapons
{
    public class MissilesFactory
    {
        private AnalyticsEventManager _analyticsEventManager;
        private ShipMissilesConfig _shipMissilesSettings;
        private GameConfig _gameConfig;
        private Transform _shipShootingPoint;
        private Pool<Missile> _missilesPool;
        private IInstantiator _instantiator;

        public MissilesFactory(
            AnalyticsEventManager analyticsEventManager,
            ShipMissilesConfig shipMissilesSettings,
            DataPersistenceHandler dataPersistenceHandler,
            ShipMovement shipMovement,
            IInstantiator instantiator
            )
        {
            _analyticsEventManager = analyticsEventManager;
            _shipMissilesSettings = shipMissilesSettings;
            _gameConfig = dataPersistenceHandler.GameConfig;
            _shipShootingPoint = shipMovement.transform.GetComponentInChildren<ShootingPoint>().transform;
            _instantiator = instantiator;

            _missilesPool = _instantiator.Instantiate<Pool<Missile>>(new object[]
            { _shipMissilesSettings.MissilePrefab, _gameConfig.MissilesPoolInitialSize });
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
}
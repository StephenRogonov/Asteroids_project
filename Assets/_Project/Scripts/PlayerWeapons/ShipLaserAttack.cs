using _Project.Scripts.Bootstrap.Configs;
using _Project.Scripts.DataPersistence;
using _Project.Scripts.Obstacles;
using _Project.Scripts.ScriptableObjects;
using System;
using System.Collections;
using UnityEngine;

namespace _Project.Scripts.PlayerWeapons
{
    public class ShipLaserAttack : MonoBehaviour
    {
        [SerializeField] private GameObject _laserBeam;

        private ShipLaserConfig _laserConfig;
        private GameConfig _gameConfig;
        private RaycastHit2D[] _obstaclesToDestroy;

        public event Action AsteroidDestroyed;
        public event Action EnemyDestroyed;

        public void Init(ShipLaserConfig shipLaserConfig, DataPersistenceHandler dataPersistenceHandler)
        {
            _laserConfig = shipLaserConfig;
            _gameConfig = dataPersistenceHandler.GameConfig;
        }

        public void PerformShot()
        {
            StartCoroutine(DisplayLaserBeam());
            HitTargetsWithLaser();
        }

        public IEnumerator DisplayLaserBeam()
        {
            _laserBeam.SetActive(true);
            yield return new WaitForSeconds(_gameConfig.LaserBeamLifetime);
            _laserBeam.SetActive(false);
        }

        private void HitTargetsWithLaser()
        {
            _obstaclesToDestroy = Physics2D.RaycastAll(transform.position, transform.up, _gameConfig.LaserDistance, _laserConfig.LayersToDestroy);
            IDamageable damageable = null;

            foreach (RaycastHit2D obstacle in _obstaclesToDestroy)
            {
                damageable = obstacle.transform.GetComponent<IDamageable>();
                CountDestroyedObstacles(damageable);
                damageable.TakeHit(HitType.Laser);
            }
        }

        private void CountDestroyedObstacles(IDamageable obstacle)
        {
            if (obstacle.ObstacleType == ObstacleType.Asteroid)
            {
                AsteroidDestroyed?.Invoke();
            }
            else if (obstacle.ObstacleType == ObstacleType.Enemy)
            {
                EnemyDestroyed?.Invoke();
            }
        }
    }
}
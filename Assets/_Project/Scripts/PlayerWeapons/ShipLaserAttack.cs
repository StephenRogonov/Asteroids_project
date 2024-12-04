using _Project.Scripts.Obstacles;
using System.Collections;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.PlayerWeapons
{
    public class ShipLaserAttack : MonoBehaviour
    {
        [SerializeField] private GameObject _laserBeam;

        private ShipLaserConfig _config;
        private RaycastHit2D[] _obstaclesToDestroy;

        private int _asteroidsDestroyed;
        private int _enemiesDestroyed;

        public int AsteroidsDestroyed => _asteroidsDestroyed;
        public int EnemiesDestroyed => _enemiesDestroyed;

        [Inject]
        private void Construct(ShipLaserConfig config)
        {
            _config = config;

            _asteroidsDestroyed = 0;
            _enemiesDestroyed = 0;
        }

        public void PerformShot()
        {
            StartCoroutine(DisplayLaserBeam());
            HitTargetsWithLaser();
        }

        public IEnumerator DisplayLaserBeam()
        {
            _laserBeam.SetActive(true);
            yield return new WaitForSeconds(_config.BeamLifetime);
            _laserBeam.SetActive(false);
        }

        private void HitTargetsWithLaser()
        {
            _obstaclesToDestroy = Physics2D.RaycastAll(transform.position, transform.up, _config.LaserDistance, _config.LayersToDestroy);
            IDamageable damageable = null;

            foreach (RaycastHit2D obstacle in _obstaclesToDestroy)
            {
                damageable = obstacle.transform.GetComponent<IDamageable>();
                CountDestroyedObstacles(damageable);
                damageable.TakeHit(WeaponType.Laser);
            }
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
}
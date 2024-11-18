using _Project.Scripts.Obstacles;
using System.Collections;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.PlayerWeapons
{
    public class ShipLaserAttack : MonoBehaviour
    {
        [SerializeField] private GameObject _laserBeam;

        private ShipShootingLaserConfig _config;
        private RaycastHit2D[] _obstaclesToDestroy;

        [Inject]
        private void Construct(ShipShootingLaserConfig config)
        {
            _config = config;
        }

        public void PerformShot()
        {
            StartCoroutine(DisplayLaserBeam());
            HitTargetsWithLaser();
        }

        private void HitTargetsWithLaser()
        {
            _obstaclesToDestroy = Physics2D.RaycastAll(transform.position, transform.up, _config.LaserDistance, _config.LayersToDestroy);

            foreach (RaycastHit2D obstacle in _obstaclesToDestroy)
            {
                obstacle.transform.GetComponent<IDamageable>().TakeHit(WeaponType.Laser);
            }
        }

        public IEnumerator DisplayLaserBeam()
        {
            _laserBeam.SetActive(true);
            yield return new WaitForSeconds(_config.BeamLifetime);
            _laserBeam.SetActive(false);
        }
    }
}
using _Project.Scripts.Obstacles;
using System.Collections;
using UnityEngine;

namespace _Project.Scripts.PlayerWeapons
{
    public class LaserBeam : MonoBehaviour
    {
        [SerializeField] private GameObject _laserBeam;
        [SerializeField] private float _laserDistance = 20f;
        [SerializeField] private float _beamLifetime = 0.2f;
        [SerializeField] LayerMask _layersToDestroy;

        private RaycastHit2D[] _obstaclesToDestroy;
        private LaserRestoration _laserRestoration;

        private void Awake()
        {
            _laserRestoration = GetComponent<LaserRestoration>();
        }

        public void ShootLaser()
        {
            if (_laserRestoration.ShotsAmount <= 0)
            {
                return;
            }

            StartCoroutine(ShowLaserBeam());

            _obstaclesToDestroy = Physics2D.RaycastAll(transform.position, transform.up, _laserDistance, _layersToDestroy);

            foreach (RaycastHit2D obstacle in _obstaclesToDestroy)
            {
                if (obstacle.transform.GetComponent<IDamageable>() != null)
                {
                    obstacle.transform.GetComponent<IDamageable>().TakeHit(WeaponType.Laser);
                }
            }

            _laserRestoration.UpdateShotsAmount(-1);
        }

        private IEnumerator ShowLaserBeam()
        {
            _laserBeam.SetActive(true);
            yield return new WaitForSeconds(_beamLifetime);
            _laserBeam.SetActive(false);
        }
    }
}
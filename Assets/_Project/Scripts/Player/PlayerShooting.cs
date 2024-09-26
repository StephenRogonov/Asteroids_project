using _Project.Scripts.Common;
using _Project.Scripts.PlayerWeapons;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Project.Scripts.Player
{
    public class PlayerShooting : MonoBehaviour
    {
        [SerializeField] private int _missilePoolInitialSize = 6;
        [SerializeField] private Missile _missilePrefab;
        [SerializeField] private Transform _shootingPoint;

        private Pool<Missile> _missilesPool;
        private LaserBeam _laserBeam;

        private void Awake()
        {
            _laserBeam = transform.GetComponentInChildren<LaserBeam>();
            _missilesPool = new Pool<Missile>(_missilePrefab, _missilePoolInitialSize);
        }

        public void Shoot(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Missile missile = _missilesPool.Get();
                missile.transform.position = _shootingPoint.position;
                missile.transform.rotation = _shootingPoint.rotation;
                missile.SetPool(_missilesPool);
                missile.gameObject.SetActive(true);
            }
        }

        public void ShootLaser(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _laserBeam.ShootLaser();
            }
        }
    }
}
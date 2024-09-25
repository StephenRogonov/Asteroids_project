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
        private PlayerInputControls _inputActions;

        private void Awake()
        {
            _inputActions = new PlayerInputControls();
            _laserBeam = transform.GetComponentInChildren<LaserBeam>();
            _missilesPool = new Pool<Missile>(_missilePrefab, _missilePoolInitialSize);
        }

        private void OnEnable()
        {
            _inputActions.Enable();

            _inputActions.Player.ShootMissile.performed += Shoot;
            _inputActions.Player.ShootLaser.performed += ShootLaser;
        }

        private void OnDisable()
        {
            _inputActions.Disable();

            _inputActions.Player.ShootMissile.performed -= Shoot;
            _inputActions.Player.ShootLaser.performed -= ShootLaser;
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
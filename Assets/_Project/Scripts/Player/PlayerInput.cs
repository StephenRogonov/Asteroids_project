using _Project.Scripts.PlayerWeapons;
using System;

namespace _Project.Scripts.Player
{
    public class PlayerInput : IDisposable
    {
        private ShipMovement _shipMovement;
        private WeaponTrigger _weaponTrigger;
        private PlayerControls _playerControls;

        public PlayerInput(PlayerControls playerControls, ShipMovement shipMovement, WeaponTrigger uiController)
        {
            _playerControls = playerControls;
            _shipMovement = shipMovement;
            _weaponTrigger = uiController;

            SubscribeToActions();
        }

        private void SubscribeToActions()
        {
            _playerControls.Enable();

            _playerControls.Player.MoveForward.performed += _shipMovement.Move;
            _playerControls.Player.MoveForward.canceled += _shipMovement.Move;

            _playerControls.Player.RotateLeft.performed += _shipMovement.PlayerRotateLeft;
            _playerControls.Player.RotateLeft.canceled += _shipMovement.PlayerRotateLeft;

            _playerControls.Player.RotateRight.performed += _shipMovement.PlayerRotateRight;
            _playerControls.Player.RotateRight.canceled += _shipMovement.PlayerRotateRight;

            _playerControls.Player.ShootMissile.performed += _weaponTrigger.ShootMissile;
            _playerControls.Player.ShootLaser.performed += _weaponTrigger.ShootLaser;
        }

        public void Dispose()
        {
            _playerControls.Disable();

            _playerControls.Player.MoveForward.performed -= _shipMovement.Move;
            _playerControls.Player.MoveForward.canceled -= _shipMovement.Move;

            _playerControls.Player.RotateLeft.performed -= _shipMovement.PlayerRotateLeft;
            _playerControls.Player.RotateLeft.canceled -= _shipMovement.PlayerRotateLeft;

            _playerControls.Player.RotateRight.performed -= _shipMovement.PlayerRotateRight;
            _playerControls.Player.RotateRight.canceled -= _shipMovement.PlayerRotateRight;

            _playerControls.Player.ShootMissile.performed -= _weaponTrigger.ShootMissile;
            _playerControls.Player.ShootLaser.performed -= _weaponTrigger.ShootLaser;
        }
    }
}
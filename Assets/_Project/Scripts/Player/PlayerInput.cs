using _Project.Scripts.PlayerWeapons;
using System;
using UnityEngine.InputSystem;

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

            _playerControls.Player.MoveForward.performed += Move;
            _playerControls.Player.MoveForward.canceled += Move;

            _playerControls.Player.RotateLeft.performed += PlayerRotateLeft;
            _playerControls.Player.RotateLeft.canceled += PlayerRotateLeft;

            _playerControls.Player.RotateRight.performed += PlayerRotateRight;
            _playerControls.Player.RotateRight.canceled += PlayerRotateRight;

            _playerControls.Player.ShootMissile.performed += ShootMissile;
            _playerControls.Player.ShootLaser.performed += ShootLaser;
        }

        public void Dispose()
        {
            _playerControls.Disable();

            _playerControls.Player.MoveForward.performed -= Move;
            _playerControls.Player.MoveForward.canceled -= Move;

            _playerControls.Player.RotateLeft.performed -= PlayerRotateLeft;
            _playerControls.Player.RotateLeft.canceled -= PlayerRotateLeft;

            _playerControls.Player.RotateRight.performed -= PlayerRotateRight;
            _playerControls.Player.RotateRight.canceled -= PlayerRotateRight;

            _playerControls.Player.ShootMissile.performed -= ShootMissile;
            _playerControls.Player.ShootLaser.performed -= ShootLaser;
        }

        private void Move(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _shipMovement.Move(true);
            }
            else if (context.canceled)
            {
                _shipMovement.Move(false);
            }
        }

        private void PlayerRotateLeft(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _shipMovement.Rotate(1f);
            }
            else if (context.canceled)
            {
                _shipMovement.Rotate(0f);
            }
        }

        private void PlayerRotateRight(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _shipMovement.Rotate(-1f);
            }
            else if (context.canceled)
            {
                _shipMovement.Rotate(0f);
            }
        }

        private void ShootMissile(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _weaponTrigger.ShootMissile();
            }
        }

        private void ShootLaser(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _weaponTrigger.ShootLaser();
            }
        }
    }
}
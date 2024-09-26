using _Project.Scripts.Player;
using _Project.Scripts.PlayerWeapons;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private ShipMovement _shipMovement;
    private PlayerShooting _playerShooting;
    private PlayerControls _playerControls;

    private void Awake()
    {
        _playerControls = new PlayerControls();
        _playerShooting = transform.GetComponentInChildren<PlayerShooting>();
        _shipMovement = GetComponent<ShipMovement>();
    }

    private void OnEnable()
    {
        _playerControls.Enable();

        _playerControls.Player.MoveForward.performed += _shipMovement.Move;
        _playerControls.Player.MoveForward.canceled += _shipMovement.Move;

        _playerControls.Player.RotateLeft.performed += _shipMovement.PlayerRotateLeft;
        _playerControls.Player.RotateLeft.canceled += _shipMovement.PlayerRotateLeft;

        _playerControls.Player.RotateRight.performed += _shipMovement.PlayerRotateRight;
        _playerControls.Player.RotateRight.canceled += _shipMovement.PlayerRotateRight;

        _playerControls.Player.ShootMissile.performed += _playerShooting.Shoot;
        _playerControls.Player.ShootLaser.performed += _playerShooting.ShootLaser;
    }

    private void OnDisable()
    {
        _playerControls.Disable();

        _playerControls.Player.MoveForward.performed -= _shipMovement.Move;
        _playerControls.Player.MoveForward.canceled -= _shipMovement.Move;

        _playerControls.Player.RotateLeft.performed -= _shipMovement.PlayerRotateLeft;
        _playerControls.Player.RotateLeft.canceled -= _shipMovement.PlayerRotateLeft;

        _playerControls.Player.RotateRight.performed -= _shipMovement.PlayerRotateRight;
        _playerControls.Player.RotateRight.canceled -= _shipMovement.PlayerRotateRight;

        _playerControls.Player.ShootMissile.performed -= _playerShooting.Shoot;
        _playerControls.Player.ShootLaser.performed -= _playerShooting.ShootLaser;
    }
}

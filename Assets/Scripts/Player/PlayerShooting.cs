using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private Transform missilePrefab;
    [SerializeField] private Transform shootingPoint;

    private LaserBeam _laserBeam;
    private InputSystem_Actions _inputActions;

    private void Awake()
    {
        _inputActions = new InputSystem_Actions();
        _laserBeam = transform.GetComponentInChildren<LaserBeam>();
    }

    public void Shoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Instantiate(missilePrefab, shootingPoint.position, shootingPoint.rotation);
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

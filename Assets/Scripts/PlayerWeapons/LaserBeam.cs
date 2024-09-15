using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    [SerializeField] private Transform laserBeamPrefab;
    [SerializeField] private float laserDistance = 20f;
    [SerializeField] private float beamLifetime = 0.2f;
    [SerializeField] string[] layersToDestroy;
    
    private RaycastHit2D[] _obstaclesToDestroy;
    private Transform _laserBeam;
    private LaserRestoration _laserAvailability;

    private void Awake()
    {
        _laserAvailability = GetComponent<LaserRestoration>();
    }

    public void ShootLaser()
    {
        if (_laserAvailability.CanShoot() == false)
        {
            return;
        }

        _laserBeam = Instantiate(laserBeamPrefab, transform);
        Destroy(_laserBeam.gameObject, beamLifetime);

        _obstaclesToDestroy = Physics2D.RaycastAll(transform.position, transform.up, laserDistance, LayerMask.GetMask(layersToDestroy));

        foreach (RaycastHit2D obstacle in _obstaclesToDestroy)
        {
            Destroy(obstacle.transform.gameObject);
        }
    }
}

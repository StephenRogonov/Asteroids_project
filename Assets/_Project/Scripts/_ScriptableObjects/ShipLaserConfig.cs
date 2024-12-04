using UnityEngine;

[CreateAssetMenu(fileName = "ShipShootingLaserConfig", menuName = "Scriptable Objects/ShipShootingLaserConfig")]
public class ShipLaserConfig : ScriptableObject
{
    public int LaserShotsStartCount = 1;
    public float LaserShotRestorationTime = 10f;
    public float BeamLifetime = 0.2f;

    public float LaserDistance = 20f;
    public LayerMask LayersToDestroy;
}

using UnityEngine;

namespace _Project.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "ShipShootingLaserConfig", menuName = "Scriptable Objects/ShipShootingLaserConfig")]
    public class ShipLaserConfig : ScriptableObject
    {
        public LayerMask LayersToDestroy;
    }
}
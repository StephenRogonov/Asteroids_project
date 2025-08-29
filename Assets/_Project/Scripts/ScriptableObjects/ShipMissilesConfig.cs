using _Project.Scripts.PlayerWeapons;
using UnityEngine;

namespace _Project.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "ShipShootingMissilesConfig", menuName = "Scriptable Objects/ShipShootingMissilesConfig")]
    public class ShipMissilesConfig : ScriptableObject
    {
        public Missile MissilePrefab;
    }
}
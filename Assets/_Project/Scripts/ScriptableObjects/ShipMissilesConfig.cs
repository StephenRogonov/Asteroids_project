using _Project.Scripts.PlayerWeapons;
using UnityEngine;

[CreateAssetMenu(fileName = "ShipShootingMissilesConfig", menuName = "Scriptable Objects/ShipShootingMissilesConfig")]
public class ShipMissilesConfig : ScriptableObject
{
    public Missile MissilePrefab;
}

using UnityEngine;

[CreateAssetMenu(fileName = "ShipMovementConfig", menuName = "Scriptable Objects/ShipMovementConfig")]
public class ShipMovementConfig : ScriptableObject
{
    [SerializeField, Range(0.1f, 20f)] private float _acceleration = 5f;
    [SerializeField, Range(0.1f, 20f)] private float _maxSpeed = 5f;
    [SerializeField, Range(0.1f, 10f)] private float _rotationSpeed = 2f;

    public float Acceleration => _acceleration;
    public float MaxSpeed => _maxSpeed;
    public float RotationSpeed => _rotationSpeed;
}

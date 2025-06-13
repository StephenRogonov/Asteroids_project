using _Project.Scripts.Enemy;
using _Project.Scripts.Obstacles;
using UnityEngine;

[CreateAssetMenu(fileName = "ObstacleSpawnerSettings", menuName = "Scriptable Objects/ObstacleSpawnerSettings")]
public class ObstacleSpawnerSettings : ScriptableObject
{
    [Header("Asteroid")]
    public Asteroid AsteroidPrefab;

    [Header("Enemy")]
    public EnemyMovement EnemyPrefab;
}

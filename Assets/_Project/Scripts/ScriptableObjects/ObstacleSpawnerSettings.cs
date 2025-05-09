using _Project.Scripts.Enemy;
using _Project.Scripts.Obstacles;
using UnityEngine;

[CreateAssetMenu(fileName = "ObstacleSpawnerSettings", menuName = "Scriptable Objects/ObstacleSpawnerSettings")]
public class ObstacleSpawnerSettings : ScriptableObject
{
    [Header("Asteroid")]
    public Asteroid AsteroidPrefab;
    //public int AsteroidsPoolInitialSize = 10;
    //public float AsteroidSpawnRate = 2f;
    //public float AsteroidAngleOffset = 20f;

    [Header("Enemy")]
    public EnemyMovement EnemyPrefab;
    //public int EnemiesPoolInitialSize = 5;
    //public float EnemySpawnRate = 10f;

    //[Header("Common")]
    //public float SpawnDistance = 13f;
}

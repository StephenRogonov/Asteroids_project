using _Project.Scripts.Obstacles.Enemy;
using _Project.Scripts.Obstacles.Asteroids;
using UnityEngine;

namespace _Project.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "ObstacleSpawnerSettings", menuName = "Scriptable Objects/ObstacleSpawnerSettings")]
    public class ObstacleSpawnerSettings : ScriptableObject
    {
        [Header("Asteroid")]
        public Asteroid AsteroidPrefab;

        [Header("Enemy")]
        public EnemyMovement EnemyPrefab;
    }
}
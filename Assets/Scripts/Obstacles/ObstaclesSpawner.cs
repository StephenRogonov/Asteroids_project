using UnityEngine;

public class ObstaclesSpawner : MonoBehaviour
{
    [SerializeField] private Transform asteroidPrefab;
    [SerializeField] private Transform enemyPrefab;

    [Header("Spawn Settings")]
    [SerializeField] private float asteroidSpawnRate = 2f;
    [SerializeField] private float enemySpawnRate = 10f;
    [SerializeField] private float spawnDistance = 13f;
    [SerializeField] private float asteroidAngleOffset = 20f;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnAsteroid), asteroidSpawnRate, asteroidSpawnRate);
        InvokeRepeating(nameof(SpawnEnemy), enemySpawnRate, enemySpawnRate);
    }

    private void SpawnAsteroid()
    {
        Vector3 spawnOffset = GetRandomSpawnPosition();
        float upDirectionAngleOffset = Random.Range(-asteroidAngleOffset, asteroidAngleOffset);
        Quaternion directionOffset = Quaternion.AngleAxis(upDirectionAngleOffset, Vector3.forward);
        Quaternion rotation = Quaternion.LookRotation(transform.forward, -spawnOffset) * directionOffset;

        Instantiate(asteroidPrefab, spawnOffset, rotation, gameObject.transform);
    }

    private void SpawnEnemy()
    {
        Instantiate(enemyPrefab, GetRandomSpawnPosition(), Quaternion.identity, gameObject.transform);
    }

    private Vector3 GetRandomSpawnPosition()
    {
        return Random.insideUnitCircle.normalized * spawnDistance;
    }
}

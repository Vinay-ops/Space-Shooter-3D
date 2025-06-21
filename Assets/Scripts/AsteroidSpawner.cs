using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [Header("Asteroid Settings")]
    public GameObject asteroidPrefab;

    [Header("Spawning Around Player")]
    public float minSpawnDistance = 60f;
    public float maxSpawnDistance = 120f;
    public float safetyAngle = 30f; // degrees to avoid in front of the player

    [Header("Spawn Timing")]
    public float spawnInterval = 1f;

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (asteroidPrefab == null)
        {
            Debug.LogError("AsteroidSpawner: No asteroid prefab assigned!");
            return;
        }

        if (player == null)
        {
            Debug.LogError("AsteroidSpawner: No GameObject with tag 'Player' found!");
            return;
        }

        InvokeRepeating(nameof(SpawnAsteroid), 0f, spawnInterval);
    }

    void SpawnAsteroid()
    {
        if (player == null) return;

        Vector3 direction;
        int maxTries = 10;
        int tries = 0;

        do
        {
            direction = Random.onUnitSphere.normalized;
            float angle = Vector3.Angle(player.forward, direction);
            if (angle > safetyAngle) break;
            tries++;
        }
        while (tries < maxTries);

        float distance = Random.Range(minSpawnDistance, maxSpawnDistance);
        Vector3 spawnPosition = player.position + direction * distance;
        Quaternion spawnRotation = Random.rotation;

        Instantiate(asteroidPrefab, spawnPosition, spawnRotation);
    }
}

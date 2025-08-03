using UnityEngine;

public class EnemySpawnArea : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 3f; // waktu antar spawn musuh (dalam detik)

    private BoxCollider2D spawnArea;
    private float timer;

    void Awake()
    {
        spawnArea = GetComponent<BoxCollider2D>();
        if (!spawnArea || !spawnArea.isTrigger)
        {
            Debug.LogWarning("BoxCollider2D harus diset sebagai Trigger dan ada di GameObject ini!");
        }

        timer = spawnInterval;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            SpawnEnemy();
            timer = spawnInterval;
        }
    }

    void SpawnEnemy()
    {
        Vector2 spawnPosition = GetRandomSpawnPositionInArea();
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

    Vector2 GetRandomSpawnPositionInArea()
    {
        Vector2 center = spawnArea.bounds.center;
        Vector2 size = spawnArea.bounds.size;

        float randomX = Random.Range(center.x - size.x / 2f, center.x + size.x / 2f);
        float randomY = Random.Range(center.y - size.y / 2f, center.y + size.y / 2f);

        return new Vector2(randomX, randomY);
    }
}

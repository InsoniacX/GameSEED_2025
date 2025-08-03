using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject[] enemyPrefabs;      // Prefab musuh yang bisa di-spawn
    public float spawnInterval = 5f;        // Waktu antar spawn
    public int maxEnemies;             // Maksimum musuh yang aktif di scene

    [Header("Area Settings")]
    public Vector2 areaSize = new Vector2(5f, 5f);   // Ukuran area spawn

    [Header("Wave Settings")]
    public bool useWaveMode = false;
    public int enemiesPerWave = 5;
    public float waveInterval = 10f;

    private float waveTimer;

    private float timer;
    private int currentEnemies;

    private void Start()
    {
        timer = spawnInterval;
    }

    private void Update()
    {
        if (useWaveMode)
        {
            waveTimer -= Time.deltaTime;

            if (waveTimer <= 0f)
            {
                StartCoroutine(SpawnWave(enemiesPerWave));
                waveTimer = waveInterval;
            }
        }
        else
        {
            timer -= Time.deltaTime;

            if (timer <= 0f && currentEnemies < maxEnemies)
            {
                SpawnEnemy();
                timer = spawnInterval;
            }
        }
    }

    private IEnumerator SpawnWave(int amount)
    {
        for (int i = 0; i < amount && currentEnemies < maxEnemies; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.3f); // jeda kecil antar musuh
        }
    }

    private void SpawnEnemy()
    {
        // Pilih musuh secara acak
        GameObject enemyToSpawn = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        // Tentukan posisi spawn dalam area
        Vector2 spawnPosition = (Vector2)transform.position + new Vector2(
            Random.Range(-areaSize.x / 2, areaSize.x / 2),
            Random.Range(-areaSize.y / 2, areaSize.y / 2)
        );

        // Spawn musuh
        GameObject enemy = Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);
        currentEnemies++;

        // Kurangi jumlah saat musuh mati
        enemy.GetComponent<EnemyHealth>().OnDeath += () => currentEnemies--;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, areaSize);
    }
}

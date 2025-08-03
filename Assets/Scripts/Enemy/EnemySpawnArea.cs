using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject[] normalEnemyPrefabs;
    public GameObject[] middleEnemyPrefabs;
    public GameObject bossPrefab;

    [Header("Spawn Control")]
    public float spawnInterval = 5f;
    public int maxEnemies;

    [Header("Score-Based Activation")]
    [SerializeField] private LogicScript logicScript;
    public int middleEnemyScoreThreshold = 500;
    public int bossScoreThreshold = 1000;

    [Header("Area Settings")]
    public Vector2 areaSize = new Vector2(5f, 5f);
    public float activationRange = 10f;
    [SerializeField] private float minSpawnDistanceToPlayer = 2f; // Jarak minimal dari pemain

    [Header("Wave Settings")]
    public bool useWaveMode = false;
    public int enemiesPerWave = 5;
    public float waveInterval = 10f;

    private float timer;
    private float waveTimer;
    private int currentEnemies;
    private bool isPlayerInRange = false;
    private bool middleEnemySpawnEnabled = false;
    private bool bossSpawnEnabled = false;

    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        logicScript = FindFirstObjectByType<LogicScript>();
        timer = spawnInterval;
        waveTimer = waveInterval;
    }

    private void Update()
    {
        if (player == null || logicScript == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        isPlayerInRange = distanceToPlayer <= activationRange;

        if (!isPlayerInRange) return;

        if (!bossSpawnEnabled && logicScript.score >= bossScoreThreshold)
        {
            bossSpawnEnabled = true;
            SpawnSpecificEnemy(bossPrefab);
            return;
        }

        if (!middleEnemySpawnEnabled && logicScript.score >= middleEnemyScoreThreshold)
        {
            middleEnemySpawnEnabled = true;
        }

        if (useWaveMode)
        {
            waveTimer -= Time.deltaTime;
            if (waveTimer <= 0f && currentEnemies < maxEnemies)
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
                SpawnRandomEnemy();
                timer = spawnInterval;
            }
        }
    }

    private IEnumerator SpawnWave(int amount)
    {
        if (currentEnemies >= maxEnemies) yield break;

        for (int i = 0; i < amount; i++)
        {
            if (currentEnemies >= maxEnemies) break;
            SpawnRandomEnemy();
            yield return new WaitForSeconds(0.3f);
        }
    }

    private void SpawnRandomEnemy()
    {
        GameObject enemyToSpawn = null;
        List<GameObject> availableEnemies = new List<GameObject>();

        availableEnemies.AddRange(normalEnemyPrefabs);

        if (middleEnemySpawnEnabled)
        {
            availableEnemies.AddRange(middleEnemyPrefabs);
        }

        if (availableEnemies.Count > 0)
        {
            enemyToSpawn = availableEnemies[Random.Range(0, availableEnemies.Count)];
        }

        if (enemyToSpawn != null)
        {
            SpawnSpecificEnemy(enemyToSpawn);
        }
    }

    private void SpawnSpecificEnemy(GameObject enemyPrefab)
    {
        // Panggil GetSpawnPosition untuk mendapatkan posisi yang valid
        Vector2 spawnPosition = GetSpawnPosition();

        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        currentEnemies++;

        EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.OnDeath += (scoreToAdd) => {
                currentEnemies--;
                logicScript.addScore(scoreToAdd);
            };
        }
        else
        {
            Debug.LogWarning("Enemy prefab tidak memiliki komponen EnemyHealth!");
        }
    }

    // Metode baru untuk mendapatkan posisi spawn yang aman
    private Vector2 GetSpawnPosition()
    {
        Vector2 spawnPosition;
        float distanceToPlayer;
        int maxAttempts = 10;
        int currentAttempt = 0;

        // Ulangi pencarian posisi hingga menemukan posisi yang cukup jauh dari pemain
        do
        {
            spawnPosition = (Vector2)transform.position + new Vector2(
                Random.Range(-areaSize.x / 2, areaSize.x / 2),
                Random.Range(-areaSize.y / 2, areaSize.y / 2)
            );
            distanceToPlayer = Vector2.Distance(spawnPosition, player.position);
            currentAttempt++;
            if (currentAttempt >= maxAttempts)
            {
                // Jika gagal setelah banyak percobaan, kembalikan posisi acak
                return spawnPosition;
            }
        } while (distanceToPlayer < minSpawnDistanceToPlayer);

        return spawnPosition;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, areaSize);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, activationRange);
    }
}
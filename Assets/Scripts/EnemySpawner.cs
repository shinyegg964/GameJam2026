using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    

    public GameObject[] enemies;
    public float minimumSpawnTime = 3f;
    public float maximumSpawnTime = 7f;

    public int maxEnemies = 1000; // limit nepřátel
    private int currentEnemies = 0;

    public float difficultyIncreaseRate = 0.95f; // zrychlování spawnu

    public float spawnRadius = 5f; // jak daleko od spawneru se spawnou

    private float timer;

    void Start()
    {
        SetTimeUntilSpawn();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f && currentEnemies < maxEnemies)
        {
            SpawnEnemy();
            SetTimeUntilSpawn();

            // postupně zrychluj spawn
            minimumSpawnTime *= difficultyIncreaseRate;
            maximumSpawnTime *= difficultyIncreaseRate;

            // aby to nespadlo na nesmyslně malé hodnoty
            minimumSpawnTime = Mathf.Clamp(minimumSpawnTime, 0.5f, 100f);
            maximumSpawnTime = Mathf.Clamp(maximumSpawnTime, 1f, 100f);
        }
    }

    void SpawnEnemy()
    {
        Vector2 spawnPos = (Vector2)transform.position + Random.insideUnitCircle * spawnRadius;

        GameObject enemy = Instantiate(enemies[Random.Range(0,enemies.Length)], spawnPos, Quaternion.identity);

        currentEnemies++;

        // když enemy umře → sníží count
        Enemy enemyScript = enemy.GetComponent<Enemy>();
        if (enemyScript != null)
        {
            enemyScript.OnDeath += () => currentEnemies--;
        }
    }

    void SetTimeUntilSpawn()
    {
        timer = Random.Range(minimumSpawnTime, maximumSpawnTime);
    }
}
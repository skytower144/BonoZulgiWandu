using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private int waveNumber;
    [SerializeField] private LayerMask layer;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int minEnemyCount, maxEnemyCount;

    private List<GameObject> currentEnemies; public List<GameObject> current_enemies => currentEnemies;
    private Bounds colliderBounds; public Bounds collider_bounds => colliderBounds;
    private Vector3 colliderCenter;

    private int totalSpawn;

    [System.NonSerialized] public bool isWaveCompleted = true;
    [System.NonSerialized] public bool isBatchLoaded = false;

    void Start()
    {
        waveNumber = 0;
        totalSpawn = 0;
        colliderBounds = GetComponent<BoxCollider2D>().bounds;
        colliderCenter = colliderBounds.center;
    }

    void Update()
    {
        if (isWaveCompleted && !GameManager.instance.playerBehaviour.IsBulletAlive()) {
            isWaveCompleted = false;
            Invoke("SpawnBatch", 1f);
        }
    }


    public void SpawnBatch()
    {
        if (waveNumber == 0)
            GameManager.instance.gameTimer.StartTimer();

        totalSpawn = Random.Range(minEnemyCount, maxEnemyCount);
        currentEnemies = new List<GameObject>();

        while (currentEnemies.Count < totalSpawn)
        {
            float randomX = Random.Range(colliderCenter.x - colliderBounds.extents.x, colliderCenter.x + colliderBounds.extents.x);
            float randomY = Random.Range(colliderCenter.y - colliderBounds.extents.y, colliderCenter.y + colliderBounds.extents.y);

            Vector2 randomPos = new Vector2(randomX, randomY);

            GameObject enemy = Instantiate(enemyPrefab, randomPos, Quaternion.identity, transform);

            if (CheckOverlap(enemy)) {
                Destroy(enemy);
                continue;
            }
            enemy.GetComponent<EnemyBehaviour2>().PlayObject();
            currentEnemies.Add(enemy);
        }
        waveNumber++;
        GameManager.instance.SetEnemyObjectControl();
        isBatchLoaded = true;
    }

    private bool CheckOverlap(GameObject enemy)
    {
        return (Physics2D.OverlapCircle(enemy.transform.localPosition, 1f, layer));
    }

    public void CheckNextWave()
    {
        totalSpawn--;
        if (totalSpawn > 0) return;

        Debug.Log($"Wave {waveNumber} Complete.");
        isWaveCompleted = true;
        isBatchLoaded = false;
    }

    public void ClearAllEnemy()
    {
        foreach (Transform enemy in transform) {
            Destroy(enemy.gameObject);
        }
        currentEnemies.Clear();
        waveNumber = 0;

        isWaveCompleted = true;
        isBatchLoaded = false;
    }
}

using System.Collections;
using UnityEngine;

public class ForbiddenTargetSpawner : MonoBehaviour
{
    [Header("Forbidden Target Settings")]
    public GameObject forbiddenTargetPrefab;

    [Tooltip("ゲーム全体で出現させたい数")]
    public int totalToSpawn = 5;

    [Header("Spawn Position Range")]
    public float minX = -1f;
    public float maxX = 1f;
    public float minY = 1f;
    public float maxY = 1f;
    public float minZ = 5f;
    public float maxZ = 6f;

    private int spawnedCount = 0;
    private bool isSpawning = false;

    public void StartSpawning()
    {
        if (isSpawning || forbiddenTargetPrefab == null || totalToSpawn <= 0) return;

        isSpawning = true;
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        // ★ GameManagerのtimeLimitを参照
        float totalDuration = GameManager.Instance != null ? GameManager.Instance.GetCurrentTime() : 30f;
        float interval = totalDuration / totalToSpawn;

        while (spawnedCount < totalToSpawn)
        {
            SpawnForbiddenTarget();
            spawnedCount++;
            yield return new WaitForSeconds(interval);
        }
    }

    private void SpawnForbiddenTarget()
    {
        Vector3 spawnPos = new Vector3(
            Random.Range(minX, maxX),
            Random.Range(minY, maxY),
            Random.Range(minZ, maxZ)
        );

        Instantiate(forbiddenTargetPrefab, spawnPos, forbiddenTargetPrefab.transform.rotation);
    }
}

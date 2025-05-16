using System.Collections;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    [Header("Spawn Stop Timing")]
    [SerializeField] private float spawnStopBeforeTime = 3f;  // 残り3秒でスポーン停止

    [Header("Virus Prefabs")]
    public GameObject normalVirusPrefab;
    public GameObject rareVirusPrefab;
    public GameObject superRareVirusPrefab;

    [Header("Spawn Settings")]
    public int totalRareToSpawn = 5;
    public int totalSuperRareToSpawn = 2;

    [Range(0f, 1f)] public float rareSpawnProbability = 0.05f;
    [Range(0f, 1f)] public float superRareSpawnProbability = 0.01f;

    public float initialSpawnInterval = 1f;
    public float minSpawnInterval = 0.2f;
    public float spawnIntervalDecreaseRate = 0.95f;
    public float speedUpInterval = 10f;

    public float minX = -1f;
    public float maxX = 1f;
    public float minY = 1f;
    public float maxY = 1f;
    public float minZ = 5f;
    public float maxZ = 6f;

    private float currentSpawnInterval;
    private bool isSpawning = false;

    private Coroutine spawnCoroutine;
    private Coroutine speedUpCoroutine;

    public void StartSpawning()
    {
        if (isSpawning) return;

        isSpawning = true;
        currentSpawnInterval = initialSpawnInterval;
        spawnCoroutine = StartCoroutine(SpawnLoop());
        speedUpCoroutine = StartCoroutine(SpeedUpLoop());
    }

    public void StopSpawning()
    {
        isSpawning = false;

        if (spawnCoroutine != null) StopCoroutine(spawnCoroutine);
        if (speedUpCoroutine != null) StopCoroutine(speedUpCoroutine);
    }

    IEnumerator SpawnLoop()
{
    while (isSpawning)
    {
        float remainingTime = GameManager.Instance.GetCurrentTime();

        // ★ 残り時間が設定値以下になったら全スポーン停止
        if (remainingTime <= spawnStopBeforeTime)
        {
            Debug.Log("残り時間が少ないためスポーン停止");
            StopSpawning();  // ← speedUpCoroutine も止められる！
            yield break;
        }

        SpawnTarget();
        yield return new WaitForSeconds(currentSpawnInterval);
    }
}


    IEnumerator SpeedUpLoop()
    {
        while (isSpawning)
        {
            yield return new WaitForSeconds(speedUpInterval);
            currentSpawnInterval *= spawnIntervalDecreaseRate;
            currentSpawnInterval = Mathf.Max(currentSpawnInterval, minSpawnInterval);
            Debug.Log($"スポーン間隔が短縮: {currentSpawnInterval}秒");
        }
    }

    void SpawnTarget()
    {
        GameObject prefabToSpawn = normalVirusPrefab;

        float remainingTime = GameManager.Instance.GetCurrentTime();
        int rareLeft = totalRareToSpawn - spawnedRareCount;
        int superRareLeft = totalSuperRareToSpawn - spawnedSuperRareCount;

        bool forcedRare = false;
        bool forcedSuperRare = false;

        if (rareLeft > 0)
        {
            float estSpawnsLeft = (remainingTime - spawnStopBeforeTime) / currentSpawnInterval;
            if (rareLeft >= estSpawnsLeft)
            {
                forcedRare = true;
            }
        }

        if (superRareLeft > 0)
        {
            float estSpawnsLeft = (remainingTime - spawnStopBeforeTime) / currentSpawnInterval;
            if (superRareLeft >= estSpawnsLeft)
            {
                forcedSuperRare = true;
            }
        }

        if (superRareLeft > 0 && (forcedSuperRare || Random.value < superRareSpawnProbability))
        {
            prefabToSpawn = superRareVirusPrefab;
            spawnedSuperRareCount++;
            Debug.Log("スーパーレアウイルス出現！");
        }
        else if (rareLeft > 0 && (forcedRare || Random.value < rareSpawnProbability))
        {
            prefabToSpawn = rareVirusPrefab;
            spawnedRareCount++;
            Debug.Log("レアウイルス出現！");
        }
        else
        {
            prefabToSpawn = normalVirusPrefab;
        }

        Vector3 spawnPos = new Vector3(
            Random.Range(minX, maxX),
            Random.Range(minY, maxY),
            Random.Range(minZ, maxZ)
        );

        GameObject newTarget = Instantiate(prefabToSpawn, spawnPos, prefabToSpawn.transform.rotation);
        Target targetScript = newTarget.GetComponent<Target>();
        if (targetScript != null)
        {
            targetScript.spawner = this;
        }
    }

    private int spawnedRareCount = 0;
    private int spawnedSuperRareCount = 0;
}

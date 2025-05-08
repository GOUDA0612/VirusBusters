using System.Collections;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    [Header("Virus Prefabs")]
    public GameObject normalVirusPrefab;
    public GameObject rareVirusPrefab;
    public GameObject superRareVirusPrefab;

    [Header("Spawn Settings")]
    public int totalRareToSpawn = 5;
    public int totalSuperRareToSpawn = 2;

    [Range(0f, 1f)] public float rareSpawnProbability = 0.05f;       // レア出現確率
    [Range(0f, 1f)] public float superRareSpawnProbability = 0.01f;  // スーパーレア出現確率

    public float initialSpawnInterval = 1f;
    public float minSpawnInterval = 0.2f;
    public float spawnIntervalDecreaseRate = 0.95f; // 減少率
    public float speedUpInterval = 10f; // 何秒ごとにスピードアップするか

    public float minX = -1f;
    public float maxX = 1f;
    public float minY = 1f;
    public float maxY = 1f;
    public float minZ = 5f;
    public float maxZ = 6f;

    private float currentSpawnInterval;
    private int spawnedRareCount = 0;
    private int spawnedSuperRareCount = 0;

    private void Start()
    {
        currentSpawnInterval = initialSpawnInterval;
        StartCoroutine(SpawnLoop());
        StartCoroutine(SpeedUpLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            SpawnTarget();
            yield return new WaitForSeconds(currentSpawnInterval);
        }
    }

    IEnumerator SpeedUpLoop()
    {
        while (true)
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

        // 残り時間を取得
        float remainingTime = GameManager.Instance.GetCurrentTime();

        // レア/スーパーレア残り数
        int rareLeft = totalRareToSpawn - spawnedRareCount;
        int superRareLeft = totalSuperRareToSpawn - spawnedSuperRareCount;

        bool forcedRare = false;
        bool forcedSuperRare = false;

        // --- 残り時間の割合を使って強制出現を判断 ---
        if (rareLeft > 0)
        {
            float estSpawnsLeft = remainingTime / currentSpawnInterval;
            if (rareLeft >= estSpawnsLeft)
            {
                forcedRare = true;
            }
        }

        if (superRareLeft > 0)
        {
            float estSpawnsLeft = remainingTime / currentSpawnInterval;
            if (superRareLeft >= estSpawnsLeft)
            {
                forcedSuperRare = true;
            }
        }

        // --- 出現ロジック ---
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

        GameObject newTarget = Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);
        Target targetScript = newTarget.GetComponent<Target>();
        if (targetScript != null)
        {
            targetScript.spawner = this;
        }
    }
}

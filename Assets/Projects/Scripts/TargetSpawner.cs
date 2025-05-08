using System.Collections;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    public GameObject targetPrefab;
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
        Vector3 spawnPos = new Vector3(
            Random.Range(minX, maxX),
            Random.Range(minY, maxY),
            Random.Range(minZ, maxZ)
        );

        GameObject newTarget = Instantiate(targetPrefab, spawnPos, Quaternion.identity);
        Target targetScript = newTarget.GetComponent<Target>();
        if (targetScript != null)
        {
            targetScript.spawner = this;
        }
    }

    // このメソッドはもう不要かも？まだ使うなら残してもOK
    public void NotifyTargetDestroyed()
    {
        // 以前はここで次を出してたが、今はループで自動なので何もしなくていい
    }
}

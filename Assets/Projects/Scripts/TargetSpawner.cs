using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    public GameObject targetPrefab;
    public float spawnInterval = 1f;

    public float minX = -1f;
    public float maxX = 1f;
    public float minY = 1f;
    public float maxY = 1f;
    public float minZ = 5f;
    public float maxZ = 6f;

    private GameObject currentTarget;

    private void Start()
    {
        SpawnTarget();
    }

    void SpawnTarget()
    {
        Vector3 spawnPos = new Vector3(
            Random.Range(minX, maxX),
            Random.Range(minY, maxY),
            Random.Range(minZ, maxZ)
        );

        currentTarget = Instantiate(targetPrefab, spawnPos, Quaternion.identity);
        Target targetScript = currentTarget.GetComponent<Target>();
        if (targetScript != null)
        {
            targetScript.spawner = this;
        }
    }

    public void NotifyTargetDestroyed()
    {
        Invoke(nameof(SpawnTarget), spawnInterval);
    }
}

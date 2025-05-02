using UnityEngine;

public class Target : MonoBehaviour
{
    public float lifespan = 5f;
    [HideInInspector] public TargetSpawner spawner;

    private void Start()
    {
        Invoke(nameof(AutoDestroy), lifespan);
    }

    private void AutoDestroy()
    {
        Debug.Log("AutoDestroy called, destroying: " + gameObject.name);
        if (spawner != null)
        {
            spawner.NotifyTargetDestroyed();
        }
        Destroy(gameObject);
    }

    private void OnMouseDown()
    {
        Debug.Log("OnMouseDown called, destroying: " + gameObject.name);

        if (spawner != null)
        {
            spawner.NotifyTargetDestroyed();
        }
        Destroy(gameObject);

        // ScoreManager.Instance.AddScore(10); // 必要ならスコア加算
    }

    private void OnDestroy()
    {
        CancelInvoke(nameof(AutoDestroy));
    }
}

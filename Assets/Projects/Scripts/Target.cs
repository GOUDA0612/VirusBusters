using UnityEngine;

public class Target : MonoBehaviour
{
    public int scoreValue = 10;
    public float DestroyTime = 5f;
    [HideInInspector] public TargetSpawner spawner;

    private void Start()
    {
        Invoke(nameof(AutoDestroy), DestroyTime);
    }

    private void AutoDestroy()
    {
        spawner?.NotifyTargetDestroyed();
        Destroy(gameObject);
    }

    private void OnMouseDown()
    {
        spawner?.NotifyTargetDestroyed();
        Destroy(gameObject);

        ScoreManager.Instance.AddScore(scoreValue);
        UIManager.Instance.AddScore(scoreValue);
    }

    private void OnDestroy()
    {
        CancelInvoke(nameof(AutoDestroy));
    }
}

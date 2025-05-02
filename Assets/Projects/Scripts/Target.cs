using UnityEngine;

public class Target : MonoBehaviour
{
    public int scoreValue = 10;
    [HideInInspector] public TargetSpawner spawner;

    private void Start()
    {
        Invoke(nameof(AutoDestroy), 5f);
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
        ScoreUIManager.Instance.AddScore(scoreValue);
    }

    private void OnDestroy()
    {
        CancelInvoke(nameof(AutoDestroy));
    }
}

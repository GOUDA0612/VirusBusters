using UnityEngine;

public class TextPulsate : MonoBehaviour
{
    [Header("Pulse Settings")]
    public float scaleSpeed = 2.0f;     // 拡大縮小の速さ
    public float scaleAmount = 0.1f;    // 拡大縮小の大きさ（±）

    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        float scale = 1 + Mathf.Sin(Time.time * scaleSpeed) * scaleAmount;
        transform.localScale = originalScale * scale;
    }
}
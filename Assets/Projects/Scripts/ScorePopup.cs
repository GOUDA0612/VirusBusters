using TMPro;
using UnityEngine;
using DG.Tweening; // ★ DOTweenを使う

public class ScorePopup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private float lifetime = 1.2f;
    [SerializeField] private float moveDistance = 50f;   // 上にどれくらい動かすか
    [SerializeField] private float fadeDuration = 0.5f;  // フェード時間

    private void Awake()
    {
        if (textMesh == null)
        {
            textMesh = GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    public void SetText(string text, Color color)
    {
        textMesh.text = text;
        textMesh.color = color;

        RectTransform rt = GetComponent<RectTransform>();
        Vector2 startPos = rt.anchoredPosition;
        Vector2 endPos = startPos + new Vector2(0, moveDistance);

        // アニメーション：上に移動しながらフェードアウト
        rt.DOAnchorPos(endPos, lifetime).SetEase(Ease.OutQuad);
        textMesh.DOFade(0f, fadeDuration).SetDelay(lifetime - fadeDuration);

        // 終了後に削除
        Destroy(gameObject, lifetime);
    }
}

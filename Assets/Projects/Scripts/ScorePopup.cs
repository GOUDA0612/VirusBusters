using TMPro;
using UnityEngine;

public class ScorePopup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private float lifetime = 1.5f;

    public void SetText(string text, Color color)
    {
        textMesh.text = text;
        textMesh.color = color;
        Destroy(gameObject, lifetime); // 一定時間後に削除
    }
}

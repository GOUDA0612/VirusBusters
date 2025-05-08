using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int score = 0;
    public float magnification = 1.5f;
    private int combo = 0;  // コンボ数

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // シーンを跨いでも残す場合
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ウイルスを倒した時に呼ぶ
    public void AddScoreWithCombo(int basePoint)
    {
        combo++;  // コンボ加算

        // 得点計算
        float point = basePoint * Mathf.Pow(magnification, combo - 1);
        score += Mathf.RoundToInt(point);

        Debug.Log($"得点: {point} (コンボ: {combo}), 合計スコア: {score}");

        // UI更新
        UIManager.Instance.AddScore(Mathf.RoundToInt(point));
    }

    // タイムアウトなどでコンボをリセットする
    public void ResetCombo()
    {
        if (combo > 0)
        {
            Debug.Log($"コンボリセット！（コンボ数: {combo} → 0）");
            combo = 0;
        }
    }

    public void SaveScore()
    {
        PlayerPrefs.SetInt("Score", score);
    }

    public void ResetScore()
    {
        score = 0;
        combo = 0;
    }
}

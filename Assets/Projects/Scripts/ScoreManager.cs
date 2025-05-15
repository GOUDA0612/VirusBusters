using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int score = 0;

    private int combo = 0;        // 現在のコンボ数
    public int maxCombo = 0;      // ★ 最大コンボ数（公開）

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
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

    if (combo > maxCombo)
    {
        maxCombo = combo;
    }

    // コンボ倍率を廃止：basePoint をそのまま加算
    score += basePoint;

    Debug.Log($"得点: {basePoint} (コンボ: {combo}), 合計スコア: {score}");

    UIManager.Instance.AddScore(basePoint);
    UIManager.Instance.UpdateCombo(combo);
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

    public void SaveScore(bool isStageCleared)
{
    score += maxCombo;

    PlayerPrefs.SetInt("Score", score);
    PlayerPrefs.SetInt("MaxCombo", maxCombo);
    PlayerPrefs.SetInt("StageCleared", isStageCleared ? 1 : 0); // ★ 成否を保存
}


    public void ResetScore()
    {
        score = 0;
        combo = 0;
        maxCombo = 0;
    }
}

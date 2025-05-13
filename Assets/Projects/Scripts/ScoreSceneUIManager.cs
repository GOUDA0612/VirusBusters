using UnityEngine;
using TMPro;

public class ScoreSceneUIManager_Hara : MonoBehaviour
{
    [Header("UI Components")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI maxComboText;

    [Header("Labels")]
    [SerializeField] private string scoreLabel = "Score: ";
    [SerializeField] private string maxComboLabel = "Max Combo: ";

    void Start()
    {
        int score = 0;
        int maxCombo = 0;

        if (ScoreManager.Instance != null)
        {
            score = ScoreManager.Instance.score;
            maxCombo = ScoreManager.Instance.maxCombo;
        }
        else
        {
            Debug.LogWarning("ScoreManager.Instance is null, fallback to PlayerPrefs");
            score = PlayerPrefs.GetInt("Score", 0);
            maxCombo = PlayerPrefs.GetInt("MaxCombo", 0);
        }

        if (scoreText != null)
        {
            scoreText.text = scoreLabel + score;
        }

        if (maxComboText != null)
        {
            maxComboText.text = maxComboLabel + maxCombo;
        }
    }
}


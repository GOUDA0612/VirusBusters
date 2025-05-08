using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI Components")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timerText;

    [Header("UI Labels")]
    [SerializeField] private string scoreLabel = "Score";
    [SerializeField] private string timeLabel = "Time";

    private int score = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateScoreText();
    }

    void Update()
    {
        // GameManagerから現在の残り時間を取得して表示
        if (timerText != null && GameManager.Instance != null && GameManager.Instance.IsGameRunning())
        {
            float currentTime = GameManager.Instance.GetCurrentTime();
            timerText.text = $"{timeLabel}{Mathf.CeilToInt(currentTime)}";
        }
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = $"{scoreLabel}{score}";
        }
    }
}

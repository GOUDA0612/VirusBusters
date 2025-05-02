using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreUIManager : MonoBehaviour
{
    public static ScoreUIManager Instance;

    [Header("UI Components")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;

    [Header("UI Display Format")]
    public string scoreFormat = "Score: {0}";
    public string timeFormat = "Time: {0}";

    [Header("Game Settings")]
    public int score = 0;
    public float timeLimit = 30f;

    private float currentTime;

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
        currentTime = timeLimit;
        UpdateScoreText();
        UpdateTimerText();
    }

    void Update()
    {
        currentTime -= Time.deltaTime;
        UpdateTimerText();

        if (currentTime <= 0f)
        {
            currentTime = 0f;
            EndGame();
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
            scoreText.text = string.Format(scoreFormat, score);
        }
    }

    private void UpdateTimerText()
    {
        if (timerText != null)
        {
            timerText.text = string.Format(timeFormat, Mathf.CeilToInt(currentTime));
        }
    }

    private void EndGame()
    {
        GameManager.Instance.EndGame();
    }
}

using UnityEngine;
using TMPro;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI Components")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timerText;

    [Header("UI Labels")]
    [SerializeField] private string scoreLabel = "Score";
    [SerializeField] private string timeLabel = "Time";

    [Header("Countdown UI")]
    [SerializeField] private TextMeshProUGUI countdownText;

    [SerializeField] private string readyText = "Ready...";
    [SerializeField] private string goText = "Go！";

    [SerializeField] private float readyDisplayTime = 1.5f;
    [SerializeField] private float goDisplayTime = 1f;

    [Header("End Text UI")]
    [SerializeField] private TextMeshProUGUI endText;
    [SerializeField] private string endMessage = "End！";
    [SerializeField] private float endDisplayTime = 2f;  // ✅ インスペクターで設定可能

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
        if (timerText != null && GameManager.Instance != null)
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

    public void StartCountdown(System.Action onComplete)
    {
        if (countdownText != null)
        {
            countdownText.gameObject.SetActive(true);
        }

        // クロスヘアを非表示
        MouseLook mouseLook = FindObjectOfType<MouseLook>();
        if (mouseLook != null)
        {
            mouseLook.SetCrosshairActive(false);
        }

        StartCoroutine(CountdownCoroutine(onComplete));
    }

    private IEnumerator CountdownCoroutine(System.Action onComplete)
    {
        if (countdownText != null)
        {
            countdownText.text = readyText;
        }

        yield return new WaitForSeconds(readyDisplayTime);

        if (countdownText != null)
        {
            countdownText.text = goText;
        }

        yield return new WaitForSeconds(goDisplayTime);

        if (countdownText != null)
        {
            countdownText.gameObject.SetActive(false);
        }

        // クロスヘアを再表示
        MouseLook mouseLook = FindObjectOfType<MouseLook>();
        if (mouseLook != null)
        {
            mouseLook.SetCrosshairActive(true);
        }

        onComplete?.Invoke();
    }

    public void ShowEndText(System.Action onComplete)
    {
        if (endText != null)
        {
            endText.text = endMessage;
            endText.gameObject.SetActive(true);
        }

        // クロスヘアを非表示
        MouseLook mouseLook = FindObjectOfType<MouseLook>();
        if (mouseLook != null)
        {
            mouseLook.SetCrosshairActive(false);
        }

        // ✅ 一定時間後にシーン遷移コールバックを呼ぶ
        StartCoroutine(EndTextCoroutine(onComplete));
    }

    private IEnumerator EndTextCoroutine(System.Action onComplete)
    {
        yield return new WaitForSeconds(endDisplayTime);

        if (endText != null)
        {
            endText.gameObject.SetActive(false);
        }

        onComplete?.Invoke();
    }
}

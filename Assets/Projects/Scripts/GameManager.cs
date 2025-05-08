using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game Settings")]
    [SerializeField] private float timeLimit = 30f;  // インスペクターで設定可
    private float currentTime;
    private bool isGameRunning = false;

    void Awake()
    {
        // Singleton パターン
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // シーンを超えて残す場合
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (isGameRunning)
        {
            currentTime -= Time.deltaTime;

            if (currentTime <= 0f)
            {
                currentTime = 0f;
                EndGame();
            }
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene_hara");
        currentTime = timeLimit;
        isGameRunning = true;
    }

    public void EndGame()
    {
        isGameRunning = false;
        SceneManager.LoadScene("ScoreScene_hara");
    }

    public void ReturnToStart()
    {
        SceneManager.LoadScene("TitleScene_hara");
    }

    // 残り時間を外部に提供
    public float GetCurrentTime()
    {
        return currentTime;
    }

    public bool IsGameRunning()
    {
        return isGameRunning;
    }
}

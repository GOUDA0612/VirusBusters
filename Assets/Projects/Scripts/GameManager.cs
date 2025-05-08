using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game Settings")]
    [SerializeField] private float timeLimit = 30f;  // インスペクターで設定可
    private float currentTime;

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

    void Start()
    {
        // シーン切り替え後も自動でタイマーがスタートする
        currentTime = timeLimit;
    }

    void Update()
    {
        currentTime -= Time.deltaTime;

        if (currentTime <= 0f)
        {
            currentTime = 0f;
            EndGame();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene_hara");
    }

    public void EndGame()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("ScoreScene_Hara");
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
}

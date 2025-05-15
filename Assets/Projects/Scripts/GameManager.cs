using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game Settings")]
    [SerializeField] private float timeLimit = 30f;

    [Header("Stage Goal Settings")]
    [SerializeField] private int goalScore = 100; // ★ ステージクリアの目標スコア

    private float currentTime;
    private bool isCountingDown = false;
    private bool isGameEnded = false;

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

    void Start()
    {
        isCountingDown = true;
        currentTime = timeLimit;

        if (UIManager.Instance != null)
        {
            UIManager.Instance.StartCountdown(OnCountdownFinished);
        }

        // ゲーム開始前はクロスヘア非表示
        SetCrosshairActive(false);

        // ゲーム開始前は視点操作を無効化
        MouseLook mouseLook = FindObjectOfType<MouseLook>();
        if (mouseLook != null)
        {
            mouseLook.SetControlEnabled(false);
        }
    }

    void Update()
    {
        if (isCountingDown || isGameEnded) return;

        currentTime -= Time.deltaTime;

        if (currentTime <= 0f)
        {
            currentTime = 0f;
            EndGame();
        }
    }

    private void OnCountdownFinished()
    {
        isCountingDown = false;
        Debug.Log("カウントダウン終了、ゲームスタート！");

        // クロスヘアを表示
        SetCrosshairActive(true);

        // 視点操作を有効化
        MouseLook mouseLook = FindObjectOfType<MouseLook>();
        if (mouseLook != null)
        {
            mouseLook.SetControlEnabled(true);
        }

        // ターゲットスポーン開始
        TargetSpawner spawner = FindObjectOfType<TargetSpawner>();
        if (spawner != null)
        {
            spawner.StartSpawning();
        }
    }

    public void EndGame()
    {
        if (isGameEnded) return;

        isGameEnded = true;

        // スポーン停止
        TargetSpawner spawner = FindObjectOfType<TargetSpawner>();
        if (spawner != null)
        {
            spawner.StopSpawning();
        }

        // マウス停止とクロスヘア非表示
        MouseLook mouseLook = FindObjectOfType<MouseLook>();
        if (mouseLook != null)
        {
            mouseLook.enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            mouseLook.SetCrosshairActive(false);
        }

        // ★ スコア目標達成判定と保存
        bool isStageCleared = ScoreManager.Instance.score >= goalScore;
        ScoreManager.Instance.SaveScore(isStageCleared);

        // 終了UI表示とシーン遷移
        if (UIManager.Instance != null)
        {
            UIManager.Instance.ShowEndText(() =>
            {
                SceneManager.LoadScene("ScoreScene_Hara");
            });
        }
        else
        {
            SceneManager.LoadScene("ScoreScene_Hara");
        }
    }

    public float GetCurrentTime()
    {
        return currentTime;
    }

    public bool IsCountingDown()
    {
        return isCountingDown;
    }

    private void SetCrosshairActive(bool isActive)
    {
        MouseLook mouseLook = FindObjectOfType<MouseLook>();
        if (mouseLook != null)
        {
            mouseLook.SetCrosshairActive(isActive);
        }
    }

    public static void DestroyInstance()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
            Instance = null;
        }
    }
}

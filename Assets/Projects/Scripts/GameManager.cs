using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game Settings")]
    [SerializeField] private float timeLimit = 30f;

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

        // スポーン開始
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

        // マウス停止
        MouseLook mouseLook = FindObjectOfType<MouseLook>();
        if (mouseLook != null)
        {
            mouseLook.enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        // Endテキスト表示 + 終了後にシーン遷移
        if (UIManager.Instance != null)
        {
            UIManager.Instance.ShowEndText(() =>
            {
                SceneManager.LoadScene("ScoreScene_Hara");
            });
        }
        else
        {
            // 保険: UIManagerがない場合は即遷移
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
}

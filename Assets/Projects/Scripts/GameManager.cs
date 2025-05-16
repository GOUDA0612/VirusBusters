using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game Settings")]
    [SerializeField] private float timeLimit = 30f;

    [Header("Stage Goal Settings")]
    [SerializeField] private int goalScore = 100;

    [Header("Tutorial Popup")]
    [SerializeField] private GameObject tutorialPopup;

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
        currentTime = timeLimit;


        if (tutorialPopup != null)
        {
            tutorialPopup.SetActive(true);           // ポップアップ表示
            SetCrosshairActive(false);               // クロスヘア非表示
            DisableMouseLook();                      // 視点操作無効

            // ★ カーソルを表示・ロック解除
            MouseLook mouseLook = FindObjectOfType<MouseLook>();
            if (mouseLook != null)
            {
                mouseLook.UnlockCursor();
            }
        }
        else
        {
            StartCountdown(); // 通常は即カウント開始
        }
    }

    void Update()
    {
        if (!isCountingDown || isGameEnded) return;

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

        SetCrosshairActive(true);
        EnableMouseLook();

        // ★ カーソルをロックして非表示に
        MouseLook mouseLook = FindObjectOfType<MouseLook>();
        if (mouseLook != null)
        {
            mouseLook.LockCursor();
        }

        TargetSpawner spawner = FindObjectOfType<TargetSpawner>();
        if (spawner != null)
        {
            spawner.StartSpawning();
        }

        ForbiddenTargetSpawner forbiddenSpawner = FindObjectOfType<ForbiddenTargetSpawner>();
        if (forbiddenSpawner != null)
        {
            forbiddenSpawner.StartSpawning();
        }
    }

    public void EndGame()
    {
        if (isGameEnded) return;
        isGameEnded = true;

        TargetSpawner spawner = FindObjectOfType<TargetSpawner>();
        if (spawner != null)
        {
            spawner.StopSpawning();
        }

        MouseLook mouseLook = FindObjectOfType<MouseLook>();
        if (mouseLook != null)
        {
            mouseLook.enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            mouseLook.SetCrosshairActive(false);
        }

        ScoreManager.Instance.sumScore();
        bool isStageCleared = ScoreManager.Instance.score >= goalScore;
        ScoreManager.Instance.SaveScore(isStageCleared);

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

    private void DisableMouseLook()
    {
        MouseLook mouseLook = FindObjectOfType<MouseLook>();
        if (mouseLook != null)
        {
            mouseLook.SetControlEnabled(false);
        }
    }

    private void EnableMouseLook()
    {
        MouseLook mouseLook = FindObjectOfType<MouseLook>();
        if (mouseLook != null)
        {
            mouseLook.SetControlEnabled(true);
        }
    }

    public void StartGameAfterTutorial()
    {
        if (!isCountingDown && !isGameEnded)
        {
            StartCoroutine(DelayedStart());
        }
    }

    private IEnumerator DelayedStart()
    {
        yield return null;
        StartCountdown();
    }

    private void StartCountdown()
    {
        SetCrosshairActive(false);
        DisableMouseLook();

        if (UIManager.Instance != null)
        {
            UIManager.Instance.StartCountdown(() =>
            {
                // カウントダウン後に実際にゲーム開始する
                isCountingDown = true;

                EnableMouseLook();

                MouseLook mouseLook = FindObjectOfType<MouseLook>();
                if (mouseLook != null)
                {
                    mouseLook.LockCursor();
                }

                TargetSpawner spawner = FindObjectOfType<TargetSpawner>();
                if (spawner != null)
                {
                    spawner.StartSpawning();
                }

                ForbiddenTargetSpawner forbiddenSpawner = FindObjectOfType<ForbiddenTargetSpawner>();
                if (forbiddenSpawner != null)
                {
                    forbiddenSpawner.StartSpawning();
                }

                SetCrosshairActive(true);
                Debug.Log("ゲームスタート：isCountingDown = true");
            });
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
    
    public bool IsGameEnded()
{
    return isGameEnded;
}

}

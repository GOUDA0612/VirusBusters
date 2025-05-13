using UnityEngine;
using UnityEngine.SceneManagement;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;

    public GameObject crosshairUI;
    public Vector2 crosshairSize = new Vector2(20f, 20f);

    private float xRotation = 0f;
    private bool isPaused = false;
    private bool isControlEnabled = false; // ★ 追加：視点操作フラグ

    void Start()
    {
        RectTransform rectTransform = crosshairUI.GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            rectTransform.sizeDelta = crosshairSize;
        }

        LockCursor();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        if (!isPaused && isControlEnabled) // ★ 追加：視点操作有効時のみ動く
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }

    void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void PauseGame()
    {
        UnlockCursor();
        isPaused = true;
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        LockCursor();
        isPaused = false;
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        UnlockCursor();
        Application.Quit();
    }

    public void GoToScene(string sceneName)
    {
        UnlockCursor();
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }

    public void SetCrosshairActive(bool active)
    {
        if (crosshairUI != null)
        {
            crosshairUI.SetActive(active);
        }
    }

    // ★ 追加：外部から視点操作ON/OFFを切り替える
    public void SetControlEnabled(bool enabled)
    {
        isControlEnabled = enabled;
    }
}

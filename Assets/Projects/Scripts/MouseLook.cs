using UnityEngine;
using UnityEngine.SceneManagement;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;

    public GameObject crosshairUI;  // CrosshairのUIオブジェクト
    public Vector2 crosshairSize = new Vector2(20f, 20f); // 幅・高さ設定

    private float xRotation = 0f;
    private bool isPaused = false;

    void Start()
    {
        if (crosshairUI != null)
        {
            crosshairUI.SetActive(false); // 最初は非表示

            // Crosshairのサイズを変更
            RectTransform rectTransform = crosshairUI.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.sizeDelta = crosshairSize;
            }
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

        if (!isPaused)
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

        if (crosshairUI != null)
        {
            crosshairUI.SetActive(true);
        }
    }

    void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (crosshairUI != null)
        {
            crosshairUI.SetActive(false);
        }
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
}

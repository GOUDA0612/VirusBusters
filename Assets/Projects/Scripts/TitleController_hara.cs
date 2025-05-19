using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleController_hara : MonoBehaviour
{
    public GameObject[] canvases;  // 6つのCanvasをここにInspectorで設定
    public Button nextButton;
    public Button backButton;
    public Button closeButton;

    private int currentIndex = 0;

    void Awake()
    {
        Debug.Log("Awake.in");
        Debug.Log(canvases.Length);
        foreach (var canvas in canvases)
        {
            if (canvas != null) canvas.SetActive(false);
            Debug.Log(canvas.name);
        }
    }

    void Start()
    {
        ShowCanvas(0); // Canvas1のみ表示
        UpdateButtons();
    }

    public void Next()
    {
        if (currentIndex < canvases.Length - 1)
        {
            currentIndex++;
            ShowCanvas(currentIndex);
            UpdateButtons();
        }
    }

    public void Back()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            ShowCanvas(currentIndex);
            UpdateButtons();
        }
    }

    private void ShowCanvas(int index)
    {
        for (int i = 0; i < canvases.Length; i++)
        {
            if (canvases[i] != null)
            {
                canvases[i].SetActive(i == index); // index番目だけ表示
            }
        }
    }

    private void UpdateButtons()
    {
        backButton.gameObject.SetActive(currentIndex > 0);
        nextButton.gameObject.SetActive(currentIndex < canvases.Length - 1);
        closeButton.gameObject.SetActive(currentIndex == canvases.Length - 1); // 最後のキャンバスでのみ表示
    }
}
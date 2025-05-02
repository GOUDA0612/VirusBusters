using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int score = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // シーンを跨いでも残す場合
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int points)
    {
        score += points;
    }

    public void SaveScore()
    {
        PlayerPrefs.SetInt("Score", score);
    }

    public void ResetScore()
    {
        score = 0;
    }
}

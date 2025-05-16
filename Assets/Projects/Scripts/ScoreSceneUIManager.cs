using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreSceneUIManager_Hara : MonoBehaviour
{
    [Header("UI Components")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI maxComboText;
    public GameObject nextStageButton; // ★ 追加：次のステージボタン
    public TextMeshProUGUI gameClearText; // ★ GameClear表示用テキスト

    [Header("Labels")]
    [SerializeField] private string scoreLabel = "Score: ";
    [SerializeField] private string maxComboLabel = "Max Combo: ";

   void Start()
{
    int score = PlayerPrefs.GetInt("Score", 0);
    int maxCombo = PlayerPrefs.GetInt("MaxCombo", 0);
    int isCleared = PlayerPrefs.GetInt("StageCleared", 0);
    string lastPlayedScene = PlayerPrefs.GetString("LastPlayedStage", "");

    bool isSecondStage = lastPlayedScene == "GameScene2_Taka";

    if (scoreText != null)
        scoreText.text = scoreLabel + score;

    if (maxComboText != null)
        maxComboText.text = maxComboLabel + maxCombo;

    if (nextStageButton != null)
        nextStageButton.SetActive(isCleared == 1 && !isSecondStage);

    if (gameClearText != null)
        gameClearText.gameObject.SetActive(isCleared == 1 && isSecondStage);
}


    }

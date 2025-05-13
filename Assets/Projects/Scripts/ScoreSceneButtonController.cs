using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreSceneButtonController : MonoBehaviour
{
    public void ReturnToTitle()
    {
        ScoreManager.Instance.ResetScore();
        GameManager.DestroyInstance();
        SceneManager.LoadScene("TitleScene_hara");
    }

    public void PlayAgain()
    {
        ScoreManager.Instance.ResetScore();
        GameManager.DestroyInstance();
        SceneManager.LoadScene("GameScene_Taka");
    }
}

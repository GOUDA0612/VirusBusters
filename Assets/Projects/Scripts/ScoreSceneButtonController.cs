using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreSceneButtonController : MonoBehaviour
{
    public void ReturnToTitle()
    {
        SceneManager.LoadScene("TitleScene_hara");
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("GameScene_hara");
    }
}

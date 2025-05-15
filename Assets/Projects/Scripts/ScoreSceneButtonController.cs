using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreSceneButtonController : MonoBehaviour
{
    [Header("Sound Effects")]
    public AudioClip titleSFX;      // タイトルボタン用SE
    public AudioClip restartSFX;    // リスタートボタン用SE
    public AudioClip nextStageSFX;    // リスタートボタン用SE
    
    public float sfxVolume = 1.0f;

    public void ReturnToTitle()
    {
        StartCoroutine(PlaySEAndLoadScene(titleSFX, "TitleScene_hara"));
    }

    public void PlayAgain()
    {
        StartCoroutine(PlaySEAndLoadScene(restartSFX, "GameScene_Taka"));
    }

    private IEnumerator PlaySEAndLoadScene(AudioClip clip, string sceneName)
    {
        if (clip != null)
        {
            GameObject tempGO = new GameObject("TempButtonAudio");
            AudioSource aSource = tempGO.AddComponent<AudioSource>();
            aSource.clip = clip;
            aSource.volume = sfxVolume;
            aSource.spatialBlend = 0f;
            aSource.Play();

            yield return new WaitForSeconds(clip.length);
            Destroy(tempGO);
        }

        ScoreManager.Instance.ResetScore();
        GameManager.DestroyInstance();
        SceneManager.LoadScene(sceneName);
    }
    
    // ★ 次ステージ遷移処理（ボタンに設定）
    public void GoToNextStage()
{
    StartCoroutine(PlaySEAndLoadScene(nextStageSFX, "GameScene2_Taka"));
}


}

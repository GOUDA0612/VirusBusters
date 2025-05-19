using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreSceneButtonController : MonoBehaviour
{
    [Header("Sound Effects")]
    public AudioClip titleSFX;       // タイトルボタン用SE
    public AudioClip restartSFX;     // リスタートボタン用SE
    public AudioClip nextStageSFX;   // 次ステージボタン用SE

    [Header("Volumes")]
    [Range(0f, 1f)] public float titleVolume = 1.0f;
    [Range(0f, 1f)] public float restartVolume = 1.0f;
    [Range(0f, 1f)] public float nextStageVolume = 1.0f;

    public void ReturnToTitle()
    {
        StartCoroutine(PlaySEAndLoadScene(titleSFX, "TitleScene_hara", titleVolume));
    }

    public void PlayAgain()
    {
        StartCoroutine(PlaySEAndLoadScene(restartSFX, "GameScene_Taka", restartVolume));
    }

    public void GoToNextStage()
    {
        StartCoroutine(PlaySEAndLoadScene(nextStageSFX, "GameScene2_Taka", nextStageVolume));
    }

    private IEnumerator PlaySEAndLoadScene(AudioClip clip, string sceneName, float volume)
    {
        if (clip != null)
        {
            GameObject tempGO = new GameObject("TempButtonAudio");
            AudioSource aSource = tempGO.AddComponent<AudioSource>();
            aSource.clip = clip;
            aSource.volume = Mathf.Clamp01(volume);  // 0.0〜1.0 に制限
            aSource.spatialBlend = 0f;
            aSource.Play();

            yield return new WaitForSeconds(clip.length);
            Destroy(tempGO);
        }

        ScoreManager.Instance.ResetScore();
        GameManager.DestroyInstance();
        SceneManager.LoadScene(sceneName);
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton_hara : MonoBehaviour
{
    public AudioClip clickSFX;           // ボタン効果音
    public float sfxVolume = 1.0f;       // 音量
    public string nextSceneName = "GameScene_Taka"; // 遷移先シーン名

    public void OnstartButtonClicked()
    {
        StartCoroutine(PlaySEAndLoadScene());
    }

    private IEnumerator PlaySEAndLoadScene()
    {
        // 一時的なAudioSourceを作成
        GameObject tempGO = new GameObject("TempButtonAudio");
        AudioSource audioSource = tempGO.AddComponent<AudioSource>();
        audioSource.clip = clickSFX;
        audioSource.volume = sfxVolume;
        audioSource.spatialBlend = 0f;
        audioSource.Play();

        // 再生完了まで待機
        yield return new WaitForSeconds(clickSFX.length);

        Destroy(tempGO);
        SceneManager.LoadScene(nextSceneName);
    }
}

using UnityEngine;

public class Target : MonoBehaviour
{
    public int scoreValue = 10;
    public float destroyTime = 5f;
    [HideInInspector] public TargetSpawner spawner;

    [Header("Sound Effects")]
    public AudioClip clickDestroySFX;
    public AudioClip timeoutDestroySFX;
    [Range(0f, 10f)] public float sfxVolume = 1.0f;

    

    private void Start()
    {
        // 指定時間後に自動で消滅
        Invoke(nameof(AutoDestroy), destroyTime);
    }

    private void AutoDestroy()
    {
        PlaySound(timeoutDestroySFX);
        Destroy(gameObject);

        // タイムアウト時はコンボをリセット
        ScoreManager.Instance.ResetCombo();
    }

    private void OnMouseDown()
    {
        PlaySound(clickDestroySFX);

        Destroy(gameObject);

        // スコア加算（コンボ付き）
        ScoreManager.Instance.AddScoreWithCombo(scoreValue);
    }

    private void OnDestroy()
    {
        // 保険でInvokeを解除
        CancelInvoke(nameof(AutoDestroy));
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip == null) return;

        GameObject tempGO = new GameObject("TempAudio");
        AudioSource aSource = tempGO.AddComponent<AudioSource>();

        aSource.clip = clip;
        aSource.volume = sfxVolume;
        aSource.spatialBlend = 0f;  // 2D音声にする
        aSource.Play();

        Destroy(tempGO, clip.length);
    }

   
}





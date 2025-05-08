using UnityEngine;

public class Target : MonoBehaviour
{
    public int scoreValue = 10;
    public float destroyTime = 5f;
    [HideInInspector] public TargetSpawner spawner;

    [Header("Sound Effects")]
    public AudioClip clickDestroySFX;
    public AudioClip timeoutDestroySFX;
    [Range(0f, 10f)] public float sfxVolume = 1.0f;  // SE音量（0～10）

    private void Start()
    {
        Invoke(nameof(AutoDestroy), destroyTime);  // 時間切れで自動消滅
    }

    private void AutoDestroy()
    {
        PlaySound(timeoutDestroySFX);  // 時間切れ音

        Destroy(gameObject);

        // タイムアウト時はコンボをリセット
        ScoreManager.Instance.ResetCombo();
    }

    private void OnMouseDown()
    {
        PlaySound(clickDestroySFX);  // クリック音

        Destroy(gameObject);

        // 倒した時はコンボ加算付きで得点を入れる
        ScoreManager.Instance.AddScoreWithCombo(scoreValue);
    }

    private void OnDestroy()
    {
        CancelInvoke(nameof(AutoDestroy));
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip == null) return;

        // 一時的なAudioSourceを生成（2D音として再生）
        GameObject tempGO = new GameObject("TempAudio");
        AudioSource aSource = tempGO.AddComponent<AudioSource>();

        aSource.clip = clip;
        aSource.volume = sfxVolume;
        aSource.spatialBlend = 0f;  // 2D音声（空間効果なし）
        aSource.Play();

        // 再生が終わったらオブジェクトを破棄
        Destroy(tempGO, clip.length);
    }
}

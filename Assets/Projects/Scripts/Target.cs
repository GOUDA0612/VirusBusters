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

    [Header("Effect")]
    [SerializeField] private GameObject destroyEffectPrefab; // ★ エフェクト用プレハブ参照

    private void Start()
    {
        // 指定時間後に自動で消滅
        Invoke(nameof(AutoDestroy), destroyTime);
    }

    private void AutoDestroy()
    {
        PlaySound(timeoutDestroySFX);
        SpawnDestroyEffect(); // ★ 自然消滅時のエフェクト（必要なら）
        Destroy(gameObject);

        // タイムアウト時はコンボをリセット
        ScoreManager.Instance.ResetCombo();
    }

    private void OnMouseDown()
    {
        PlaySound(clickDestroySFX);
        SpawnDestroyEffect(); // ★ クリック破壊時のエフェクト
        Destroy(gameObject);

        // スコア加算（コンボ付き）
        ScoreManager.Instance.AddScoreWithCombo(scoreValue);

        // スコアポップアップ（任意）
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        UIManager.Instance?.ShowScorePopup("+" + scoreValue, screenPos, Color.green);
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

    private void SpawnDestroyEffect()
    {
        if (destroyEffectPrefab != null)
        {
            Instantiate(destroyEffectPrefab, transform.position, Quaternion.identity);
        }
    }
}

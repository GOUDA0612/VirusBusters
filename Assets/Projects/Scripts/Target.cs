using UnityEngine;

public class Target : MonoBehaviour
{
    public int scoreValue = 10;
    public float destroyTime = 5f;
    [HideInInspector] public TargetSpawner spawner;

    [Header("Sound Effects")]
    public AudioClip clickDestroySFX;
    public AudioClip timeoutDestroySFX;

    [Range(0f, 1f)] public float clickVolume = 1.0f;      // ★ クリック用音量
    [Range(0f, 1f)] public float timeoutVolume = 1.0f;    // ★ タイムアウト用音量

    [Header("Effect")]
    [SerializeField] private GameObject destroyEffectPrefab;

    private void Start()
    {
        Invoke(nameof(AutoDestroy), destroyTime);
    }

    private void AutoDestroy()
    {
        PlaySound(timeoutDestroySFX, timeoutVolume); // ★ タイムアウト音量
        Destroy(gameObject);
        ScoreManager.Instance.ResetCombo();
    }

    private void OnMouseDown()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGameEnded())
            return;

        PlaySound(clickDestroySFX, clickVolume); // ★ クリック音量
        SpawnDestroyEffect();
        Destroy(gameObject);
        ScoreManager.Instance.AddScoreWithCombo(scoreValue);
        UIManager.Instance?.ShowScorePopup("+" + scoreValue, Color.green);
    }

    private void OnDestroy()
    {
        CancelInvoke(nameof(AutoDestroy));
    }

    private void PlaySound(AudioClip clip, float volume) // ★ 第2引数にvolume追加
    {
        if (clip == null) return;

        GameObject tempGO = new GameObject("TempAudio");
        AudioSource aSource = tempGO.AddComponent<AudioSource>();

        aSource.clip = clip;
        aSource.volume = volume;  // ★ 個別音量
        aSource.spatialBlend = 0f;
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

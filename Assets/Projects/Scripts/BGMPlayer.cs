using UnityEngine;

public class BGMPlayer : MonoBehaviour
{
    [Header("BGM Settings")]
    public AudioClip bgmClip;
    
    [Range(0f, 1f)]
    public float volume = 1.0f;  // ★ 追加：音量をInspectorから調整可能に

    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = bgmClip;
        audioSource.loop = true;
        audioSource.volume = volume; // ★ 音量設定
        audioSource.Play();
    }

    void Update()
    {
        // ★ Inspectorからリアルタイム反映したい場合はここで更新
        audioSource.volume = volume;
    }
}

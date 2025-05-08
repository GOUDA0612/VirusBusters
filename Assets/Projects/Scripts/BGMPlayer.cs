using UnityEngine;

public class BGMPlayer : MonoBehaviour
{
    [Header("BGM Settings")]
    public AudioClip bgmClip;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = bgmClip;
        audioSource.loop = true;
        audioSource.Play();
    }
}

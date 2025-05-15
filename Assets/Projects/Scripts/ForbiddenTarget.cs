using UnityEngine;

public class ForbiddenTarget : MonoBehaviour
{
    [Header("Penalty Settings")]
    public int penaltyScore = 30; // 減点するスコア
    public AudioClip errorSFX;    // 誤爆時のSE
    [Range(0f, 10f)] public float sfxVolume = 1.0f;

    public float destroyTime = 5f;

    private void Start()
    {
        // 時間が経ったら自然に消える（何もしない）
        Invoke(nameof(AutoDestroy), destroyTime);
    }

    private void AutoDestroy()
    {
        // 何もせずに消滅（ペナルティなし）
        Destroy(gameObject);
    }

    private void OnMouseDown()
{
    ScoreManager.Instance.ResetCombo();
    ScoreManager.Instance.score -= penaltyScore;
    if (ScoreManager.Instance.score < 0) ScoreManager.Instance.score = 0;

    PlaySound(errorSFX);
    Destroy(gameObject);

    // ポップアップ表示（赤文字でマイナス）
    Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
    UIManager.Instance?.ShowScorePopup("-" + penaltyScore, screenPos, Color.red);

    UIManager.Instance?.UpdateScoreText();
    UIManager.Instance?.UpdateCombo(0);
}




    private void OnDestroy()
    {
        CancelInvoke(nameof(AutoDestroy));
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip == null) return;

        GameObject tempGO = new GameObject("TempErrorSFX");
        AudioSource aSource = tempGO.AddComponent<AudioSource>();

        aSource.clip = clip;
        aSource.volume = sfxVolume;
        aSource.spatialBlend = 0f;
        aSource.Play();

        Destroy(tempGO, clip.length);
    }
}

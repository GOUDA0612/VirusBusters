using UnityEngine;
using TMPro;
using DG.Tweening;

public class GameClearTextEffect : MonoBehaviour
{
    [Header("References")]
    public TextMeshProUGUI clearText;

    [Header("Sound Effects")]
    public AudioClip clearSE1;
    [Range(0f, 1f)] public float volume1 = 1.0f;

    public AudioClip clearSE2;
    [Range(0f, 1f)] public float volume2 = 1.0f;

    [Header("Pulse Settings")]
    public float pulseScaleAmount = 0.1f;
    public float pulseSpeed = 1.5f;

    private Tween pulseTween;

    public void PlayClearEffect()
    {
        clearText.gameObject.SetActive(true);
        clearText.transform.localScale = Vector3.zero;
        clearText.transform.rotation = Quaternion.identity;

        Vector3 soundPos = Camera.main != null ? Camera.main.transform.position : Vector3.zero;

        // SE1 再生
        if (clearSE1 != null)
        {
            GameObject temp1 = new GameObject("TempSE1");
            AudioSource a1 = temp1.AddComponent<AudioSource>();
            a1.clip = clearSE1;
            a1.volume = volume1;
            a1.spatialBlend = 0f;
            a1.Play();
            Destroy(temp1, clearSE1.length);
        }

        // SE2 再生
        if (clearSE2 != null)
        {
            GameObject temp2 = new GameObject("TempSE2");
            AudioSource a2 = temp2.AddComponent<AudioSource>();
            a2.clip = clearSE2;
            a2.volume = volume2;
            a2.spatialBlend = 0f;
            a2.Play();
            Destroy(temp2, clearSE2.length);
        }

        // アニメーション（回転＋拡大）
        Sequence seq = DOTween.Sequence();
        seq.Append(clearText.transform.DOScale(1.2f, 0.5f).SetEase(Ease.OutBack));
        seq.Join(clearText.transform.DORotate(new Vector3(0, 0, 720), 0.5f, RotateMode.FastBeyond360));
        seq.Append(clearText.transform.DOScale(1.0f, 0.2f).SetEase(Ease.OutBounce));
        seq.OnComplete(StartPulseEffect);
    }

    private void StartPulseEffect()
    {
        pulseTween = clearText.transform
            .DOScale(new Vector3(1 + pulseScaleAmount, 1 + pulseScaleAmount, 1), pulseSpeed)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }

    public void StopPulseEffect()
    {
        if (pulseTween != null && pulseTween.IsActive())
            pulseTween.Kill();
    }
}

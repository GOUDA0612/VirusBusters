using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIMouseController : MonoBehaviour
{
    public float angleDeg = 45f;       // 角度（度数法）
    public float moveDistance = 2f;    // 移動距離
    public float duration = 1f;
    void Start()
    {
        // 角度をラジアンに変換し、方向ベクトルを作る
        float angleRad = angleDeg * Mathf.Deg2Rad;
        Vector3 direction = new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad), 0f);
        
        // 指定距離だけその方向に移動
        transform.DOMove(transform.position + direction * moveDistance, duration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

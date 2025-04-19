using UnityEngine;
using DG.Tweening;

public class JellyAnimation : MonoBehaviour
{
    public float duration = 1.5f; // 果凍動畫的持續時間
    public float strength = 0.8f; // 果凍效果的強度
    public int vibrato = 8; // 震動次數
    public float elasticity = 0.5f; // 彈性
    public float interval = 2.5f; // 觸發果凍動畫的間隔時間
    public float shakeDuration = 1.0f; // 左右晃動的持續時間
    public float fallDuration = 1.0f; // 掉下去的持續時間
    public float fallDistance = 5.0f; // 掉下去的距離

    private void Start()
    {
        // 每隔 interval 秒觸發一次 PlayJellyAnimation 方法
        InvokeRepeating("PlayJellyAnimation", 0, interval);

        // 5 秒後觸發左右晃動並掉下去的動畫
       // Invoke("ShakeAndFall", 5.0f);
    }

    public void PlayJellyAnimation()
    {
        // 重置縮放
        transform.localScale = Vector3.one;

        // 播放果凍動畫
        transform.DOPunchScale(Vector3.one * strength, duration, vibrato, elasticity)
            .SetEase(Ease.InOutQuad);
    }

    public void ShakeAndFall()
    {
        // 停止所有正在進行的動畫
        DOTween.Kill(transform);

        // 創建一個序列動畫
        Sequence sequence = DOTween.Sequence();

        // 添加左右晃動動畫
        sequence.Append(transform.DOShakePosition(shakeDuration, new Vector3(1, 0, 0), 10, 90, false, true));

        // 添加掉下去的動畫
        sequence.Append(transform.DOMoveY(transform.position.y - fallDistance, fallDuration).SetEase(Ease.InQuad));
    }
}
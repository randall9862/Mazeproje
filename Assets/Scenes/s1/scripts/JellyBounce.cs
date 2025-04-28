using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyBounce : MonoBehaviour
{
    public float scaleSpeed = 2.0f; // 縮放速度
    public float maxScale = 1.2f; // 最大縮放
    public float minScale = 0.8f; // 最小縮放

    public float swaySpeed = 10.0f; // 快速搖晃速度
    public float swayAmount = 0.5f; // 搖晃幅度
    public float fallSpeed = 5.0f; // 掉落速度

    private Vector3 originalScale;
    private Vector3 originalPosition;
    private bool scalingUp = true;
    private bool canSwayAndFall = false;

    void Start()
    {
        originalScale = transform.localScale;
        originalPosition = transform.position;
        //Shakeanddrop();
    }

    void Update()
    {
        // 縮放動畫
        float scale = scalingUp ? maxScale : minScale;
        float t = Mathf.PingPong(Time.time * scaleSpeed, 1.0f);
        float smoothScale = Mathf.SmoothStep(minScale, maxScale, t);
        transform.localScale = originalScale * smoothScale;

        if (t >= 0.99f)
        {
            scalingUp = !scalingUp;
        }

        // 左右搖晃和掉落
        if (canSwayAndFall)
        {
            SwayAndFall();
        }
    }

    // 協程：延遲開始搖晃和掉落
    IEnumerator StartSwayAndFallAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        canSwayAndFall = true;
    }

    // 左右搖晃和掉落方法
    void SwayAndFall()
    {
        float sway = Mathf.Sin(Time.time * swaySpeed) * swayAmount;
        transform.position = new Vector3(originalPosition.x + sway, transform.position.y - fallSpeed * Time.deltaTime, originalPosition.z);
    }
    public void Shakeanddrop()
    {
       StartCoroutine(StartSwayAndFallAfterDelay(.5f)); // 幾秒後開始搖晃和掉落
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Relicmagnification : MonoBehaviour//遺物放大
{
    public Transform targetPosition; // 用來儲存目標物件的Transform組件
    public RectTransform uiPosition; // UI元素的RectTransform組件
    public float moveSpeed = 5f; // 移動速度
    public float scaleSpeed = 3f; // 縮放速度
    public float targetScale = 1.5f; // 目標放大倍數

    private Vector3 originalPosition; // 儲存原始位置
    private Vector3 originalScale; // 儲存原始大小
    private bool canRestore = false; // 是否可以恢復
    private bool isAnimating = false; // 是否正在動畫中

    void Start()
    {
        if (uiPosition != null)
        {
            // 保存原始位置和大小
            originalPosition = uiPosition.position;
            originalScale = uiPosition.localScale;           
        }
    }

    void Update()
    {
        // 檢查是否可以恢復且有點擊
        if (canRestore && Input.GetMouseButtonDown(0) && !isAnimating)
        {
            StartCoroutine(RestoreUI());
        }
    }
    public void swichfication()
    {
        StartCoroutine(MoveAndScaleUI());//方法
    }

    IEnumerator MoveAndScaleUI()
    {
        isAnimating = true;
        Vector3 startPosition = uiPosition.position;
        Vector3 startScale = uiPosition.localScale;
        Vector3 targetScale = Vector3.one * this.targetScale;
        float moveProgress = 0f;
        float scaleProgress = 0f;

        while (moveProgress < 1f || scaleProgress < 1f)
        {
            // 更新移動
            if (moveProgress < 1f)
            {
                moveProgress += Time.deltaTime * moveSpeed;
                uiPosition.position = Vector3.Lerp(startPosition, targetPosition.position, moveProgress);
            }

            // 更新縮放
            if (scaleProgress < 1f)
            {
                scaleProgress += Time.deltaTime * scaleSpeed;
                uiPosition.localScale = Vector3.Lerp(startScale, targetScale, scaleProgress);
            }

            yield return null;
        }

        isAnimating = false;
        // 等待3秒後允許恢復
        yield return new WaitForSeconds(3f);
        canRestore = true;
    }

    IEnumerator RestoreUI()
    {
        isAnimating = true;
        Vector3 startPosition = uiPosition.position;
        Vector3 startScale = uiPosition.localScale;
        float moveProgress = 0f;
        float scaleProgress = 0f;

        while (moveProgress < 1f || scaleProgress < 1f)
        {
            // 更新移動
            if (moveProgress < 1f)
            {
                moveProgress += Time.deltaTime * moveSpeed;
                uiPosition.position = Vector3.Lerp(startPosition, originalPosition, moveProgress);
            }

            // 更新縮放
            if (scaleProgress < 1f)
            {
                scaleProgress += Time.deltaTime * scaleSpeed;
                uiPosition.localScale = Vector3.Lerp(startScale, originalScale, scaleProgress);
            }

            yield return null;
        }

        isAnimating = false;
        canRestore = false;
    }
}

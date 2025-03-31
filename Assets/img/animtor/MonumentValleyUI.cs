// 文件路径: Assets/Scripts/MonumentValleyUI.cs
using UnityEngine;
using UnityEngine.UI;

public class MonumentValleyUI : MonoBehaviour
{
    public GameObject uiElement; // 要显示的UI元素
    public float moveDistance = 100f; // 移动距离
    public float moveDuration = 1f; // 移动持续时间

    void Start()
    {
        // ... existing code ...
        AnimateUIElement();
        // ... existing code ...
    }

    void AnimateUIElement()
    {
        // ... existing code ...
        RectTransform rectTransform = uiElement.GetComponent<RectTransform>();
        CanvasGroup canvasGroup = uiElement.GetComponent<CanvasGroup>(); // 获取 CanvasGroup 组件

        // 检查 CanvasGroup 是否存在
        if (canvasGroup == null)
        {
            canvasGroup = uiElement.AddComponent<CanvasGroup>(); // 如果不存在，则添加 CanvasGroup
        }

        Vector2 targetPosition = rectTransform.anchoredPosition + new Vector2(0, moveDistance); // 使用 Vector2
        StartCoroutine(MoveAndFadeUIElement(rectTransform, canvasGroup, targetPosition));
        // ... existing code ...
    }

    System.Collections.IEnumerator MoveAndFadeUIElement(RectTransform rectTransform, CanvasGroup canvasGroup, Vector2 targetPosition)
    {
        // ... existing code ...
        float elapsedTime = 0f;
        Vector2 startingPosition = rectTransform.anchoredPosition;

        // 初始化透明度
        canvasGroup.alpha = 0f; // 设置初始透明度为0
        canvasGroup.interactable = false; // 禁用交互

        while (elapsedTime < moveDuration)
        {
            // 移动
            rectTransform.anchoredPosition = Vector2.Lerp(startingPosition, targetPosition, (elapsedTime / moveDuration));
            // 渐变透明度
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, (elapsedTime / moveDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rectTransform.anchoredPosition = targetPosition; // 确保最终位置
        canvasGroup.alpha = 1f; // 确保最终透明度为1
        canvasGroup.interactable = true; // 启用交互
    }
}
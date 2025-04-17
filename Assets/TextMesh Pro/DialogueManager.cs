using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class DialogueManager : MonoBehaviour
{
    public TextMeshPro dialogueText; // 這是3D空間中的TextMesh Pro組件
    public float fadeDuration = 1f; // 漸變時間
    public float typewriterSpeed = 0.05f; // 每個字母的顯示時間
    public float delayBeforeFadeOut = 3f; // 顯示完成後延遲幾秒再淡出

    private void Start()
    {
        // 測試逐字顯示效果 + 3秒後淡出
        StartCoroutine(FadeInText("hello!"));
    }

    // 逐字顯示（Typewriter Effect）
    private IEnumerator TypewriterEffect(string text)
    {
        dialogueText.text = ""; // 清空文字

        // 一個一個字顯示
        foreach (char letter in text)
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typewriterSpeed);
        }

        // 等待一段時間再淡出
        yield return new WaitForSeconds(delayBeforeFadeOut);

        // 開始漸變透明
        dialogueText.DOFade(0f, fadeDuration);

        // 等淡出結束再清空文字
        yield return new WaitForSeconds(fadeDuration);
        dialogueText.text = "";
    }

    // 可選的：單獨使用淡入文字
    private IEnumerator FadeInText(string text)
    {
        dialogueText.text = text;
        dialogueText.color = new Color(dialogueText.color.r, dialogueText.color.g, dialogueText.color.b, 0); // 初始透明
        dialogueText.DOFade(1f, fadeDuration);
        // 等待一段時間再淡出
        yield return new WaitForSeconds(delayBeforeFadeOut);

        // 開始漸變透明
        dialogueText.DOFade(0f, fadeDuration);

        yield return new WaitForSeconds(fadeDuration);

        dialogueText.text = "";
    }
}

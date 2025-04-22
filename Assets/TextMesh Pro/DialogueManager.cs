using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class DialogueManager : MonoBehaviour
{
    public TextMeshPro dialogueText; // �o�O3D�Ŷ�����TextMesh Pro�ե�
    public float fadeDuration = 1f; // ���ܮɶ�
    public float typewriterSpeed = 0.05f; // �C�Ӧr������ܮɶ�
    public float delayBeforeFadeOut = 3f; // ��ܧ����᩵��X��A�H�X

    private void Start()
    {
        // ���ճv�r��ܮĪG + 3���H�X
        StartCoroutine(FadeInText("hello!"));
    }

    // �v�r��ܡ]Typewriter Effect�^
    private IEnumerator TypewriterEffect(string text)
    {
        dialogueText.text = ""; // �M�Ť�r

        // �@�Ӥ@�Ӧr���
        foreach (char letter in text)
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typewriterSpeed);
        }

        // ���ݤ@�q�ɶ��A�H�X
        yield return new WaitForSeconds(delayBeforeFadeOut);

        // �}�l���ܳz��
        dialogueText.DOFade(0f, fadeDuration);

        // ���H�X�����A�M�Ť�r
        yield return new WaitForSeconds(fadeDuration);
        dialogueText.text = "";
    }

    // �i�諸�G��W�ϥβH�J��r
    private IEnumerator FadeInText(string text)
    {
        dialogueText.text = text;
        dialogueText.color = new Color(dialogueText.color.r, dialogueText.color.g, dialogueText.color.b, 0); // ��l�z��
        dialogueText.DOFade(1f, fadeDuration);
        // ���ݤ@�q�ɶ��A�H�X
        yield return new WaitForSeconds(delayBeforeFadeOut);

        // �}�l���ܳz��
        dialogueText.DOFade(0f, fadeDuration);

        yield return new WaitForSeconds(fadeDuration);

        dialogueText.text = "";
    }
}

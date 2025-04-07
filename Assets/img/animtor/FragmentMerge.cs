using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FragmentMerge : MonoBehaviour
{
    public Image[] fragments; // ӸH
    public float scaleDuration = 1f; // ��j�ɶ�
    public Vector3 enlargedScale = new Vector3(1.5f, 1.5f, 1f); // ��j���

    void Start()
    {
        // ���Ҧ��H���ܳz��
        foreach (var fragment in fragments)
        {
            Color color = fragment.color;
            color.a = 0.3f; // �]�w�z���׬� 0
            fragment.color = color;
        }
    }

    public void StartEnlargeEffect(int fragmentIndex)
    {
        if (fragmentIndex >= 0 && fragmentIndex < fragments.Length)
        {
            StartCoroutine(DelayedEnlargeEffect(fragmentIndex));
        }
    }

    private IEnumerator DelayedEnlargeEffect(int fragmentIndex)
    {
        yield return new WaitForSeconds(2.5f); // 等待 2 秒
        StartCoroutine(EnlargeAndFadeIn(fragments[fragmentIndex]));
    }

    IEnumerator EnlargeAndFadeIn(Image fragment)
    {
        float elapsedTime = 0f;
        Vector3 originalScale = fragment.transform.localScale;
        Color color = fragment.color;

        while (elapsedTime < scaleDuration)
        {
            fragment.transform.localScale = Vector3.Lerp(originalScale, enlargedScale, elapsedTime / scaleDuration);
            color.a = Mathf.Lerp(0f, 1f, elapsedTime / scaleDuration);
            fragment.color = color;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        fragment.transform.localScale = originalScale;
        color.a = 1f;
        fragment.color = color;
    }
}

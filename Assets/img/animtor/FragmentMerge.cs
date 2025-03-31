using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FragmentMerge : MonoBehaviour
{
    public Image[] fragments; // 三個碎片
    public float scaleDuration = 1f; // 放大時間
    public Vector3 enlargedScale = new Vector3(1.5f, 1.5f, 1f); // 放大比例

    void Start()
    {
        // 讓所有碎片變透明
        foreach (var fragment in fragments)
        {
            Color color = fragment.color;
            color.a = 0.3f; // 設定透明度為 0
            fragment.color = color;
        }
    }

    public void StartEnlargeEffect(int fragmentIndex)
    {
        if (fragmentIndex >= 0 && fragmentIndex < fragments.Length)
        {
            StartCoroutine(EnlargeAndFadeIn(fragments[fragmentIndex]));
        }
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

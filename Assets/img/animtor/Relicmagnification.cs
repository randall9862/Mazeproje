using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RelicMagnification : MonoBehaviour
{
    public RectTransform uiPosition;         
    public float moveSpeed = 5f;             
    public float scaleSpeed = 3f;            
    public float targetScale = 1.5f;         

    private Vector3 originalPosition;        
    private Vector3 originalScale;           
    private bool canRestore = false;         
    private bool isAnimating = false;        

    void Start()
    {
        if (uiPosition != null)
        {
            originalPosition = uiPosition.position;
            originalScale = uiPosition.localScale;
        }
        else
        {
            Debug.LogWarning("UI Position (RectTransform) is not assigned.");
        }
        
    }

    void Update()
    {
        if (canRestore && Input.GetMouseButtonDown(0) && !isAnimating)
        {
            StartCoroutine(RestoreUI());
        }
    }

    // 修改後的 SwitchMagnification 方法
    public void SwitchMagnification()
    {
        if (uiPosition != null)
        {
            StartCoroutine(DelayedMagnification());
        }
    }

    // 新增延遲執行的協程
    private IEnumerator DelayedMagnification()
    {
        yield return new WaitForSeconds(3f); // 等待 2 秒
        StartCoroutine(MoveAndScaleUI());
    }

    IEnumerator MoveAndScaleUI()
    {
        isAnimating = true;

        Vector3 startPosition = uiPosition.position;
        Vector3 startScale = uiPosition.localScale;
        Vector3 targetScaleVector = Vector3.one * targetScale;

        Vector3 targetScreenPos = new Vector3(Screen.width / 2, Screen.height / 2, 0);

        float moveProgress = 0f;
        float scaleProgress = 0f;

        while (moveProgress < 1f || scaleProgress < 1f)
        {
            if (moveProgress < 1f)
            {
                moveProgress += Time.deltaTime * moveSpeed;
                uiPosition.position = Vector3.Lerp(startPosition, targetScreenPos, moveProgress);
            }

            if (scaleProgress < 1f)
            {
                scaleProgress += Time.deltaTime * scaleSpeed;
                uiPosition.localScale = Vector3.Lerp(startScale, targetScaleVector, scaleProgress);
            }

            yield return null;
        }

        isAnimating = false;
        yield return new WaitForSeconds(.5f);//點擊恢復結束
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
            if (moveProgress < 1f)
            {
                moveProgress += Time.deltaTime * moveSpeed;
                uiPosition.position = Vector3.Lerp(startPosition, originalPosition, moveProgress);
            }

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
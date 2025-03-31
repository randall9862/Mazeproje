using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Relicmagnification : MonoBehaviour//�򪫩�j
{
    public Transform targetPosition; // �Ψ��x�s�ؼЪ���Transform�ե�
    public RectTransform uiPosition; // UI������RectTransform�ե�
    public float moveSpeed = 5f; // ���ʳt��
    public float scaleSpeed = 3f; // �Y��t��
    public float targetScale = 1.5f; // �ؼЩ�j����

    private Vector3 originalPosition; // �x�s��l��m
    private Vector3 originalScale; // �x�s��l�j�p
    private bool canRestore = false; // �O�_�i�H��_
    private bool isAnimating = false; // �O�_���b�ʵe��

    void Start()
    {
        if (uiPosition != null)
        {
            // �O�s��l��m�M�j�p
            originalPosition = uiPosition.position;
            originalScale = uiPosition.localScale;           
        }
    }

    void Update()
    {
        // �ˬd�O�_�i�H��_�B���I��
        if (canRestore && Input.GetMouseButtonDown(0) && !isAnimating)
        {
            StartCoroutine(RestoreUI());
        }
    }
    public void swichfication()
    {
        StartCoroutine(MoveAndScaleUI());//��k
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
            // ��s����
            if (moveProgress < 1f)
            {
                moveProgress += Time.deltaTime * moveSpeed;
                uiPosition.position = Vector3.Lerp(startPosition, targetPosition.position, moveProgress);
            }

            // ��s�Y��
            if (scaleProgress < 1f)
            {
                scaleProgress += Time.deltaTime * scaleSpeed;
                uiPosition.localScale = Vector3.Lerp(startScale, targetScale, scaleProgress);
            }

            yield return null;
        }

        isAnimating = false;
        // ����3��᤹�\��_
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
            // ��s����
            if (moveProgress < 1f)
            {
                moveProgress += Time.deltaTime * moveSpeed;
                uiPosition.position = Vector3.Lerp(startPosition, originalPosition, moveProgress);
            }

            // ��s�Y��
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

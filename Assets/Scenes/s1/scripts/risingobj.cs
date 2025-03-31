// YourScript.cs
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class risingobj : MonoBehaviour
{
    public float height1 = 2f; // �Ĥ@�Ӫ��󪺦ۭq����
    public float height2 = 3f; // �ĤG�Ӫ��󪺦ۭq����
    public float duration = 1f; // �ʵe����ɶ�

    public GameObject object1; // �Ĥ@�Ӫ���
    public GameObject object2; // �ĤG�Ӫ���

    private float originalY1; // �Ĥ@�Ӫ��󪺭�lY��m
    private float originalY2; // �ĤG�Ӫ��󪺭�lY��m

    private void Start()
    {
        // �O�����󪺭�lY��m
        originalY1 = object1.transform.position.y;
        originalY2 = object2.transform.position.y;
    }

    private void Update()
    {
        // �˴����a���ưʿ�J
        if (Input.GetMouseButtonDown(0)) // �����a���U�ƹ�����
        {
            // �ˬd�ưʤ�V
            if (Input.GetAxis("Mouse Y") > 0) // �V�W�ư�
            {
                MoveObject(object1, Mathf.RoundToInt(height1));
                MoveObject(object2, Mathf.RoundToInt(height2));
            }
            else if (Input.GetAxis("Mouse Y") < 0) // �V�U�ư�
            {
                MoveObject(object1, -Mathf.RoundToInt(height1)); // �U��
                MoveObject(object2, -Mathf.RoundToInt(height2)); // �U��
            }
        }
    }

    private void MoveObject(GameObject obj, int height)
    {
        // �p��s��Y��m
        float newY;

        if (height > 0) // 向上滑動
        {
            newY = originalY1 + height; // 對於 object1
            if (obj == object2) newY = originalY2 + height; // 對於 object2
        }
        else // 向下滑動
        {
            newY = obj == object1 ? originalY1 : originalY2; // 恢復原始位置
        }

        // �T�O���󤣷|�C���l��m
        if (newY >= originalY1 && obj == object1 || newY >= originalY2 && obj == object2)
        {
            // ����W�ɩΤU��
            obj.transform.DOMoveY(newY, duration);
        }
    }
}
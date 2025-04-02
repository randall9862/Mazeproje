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
    public float minSwipeDistance = 50f; // 最小滑動距離

    public GameObject object1; // Ĥ@Ӫ���
    public GameObject object2; // �ĤG�Ӫ���

    private float originalY1; // �Ĥ@�Ӫ��󪺭�lY��m
    private float originalY2; // �ĤG�Ӫ��󪺭�lY��m
    private bool isAnimating = false;
    private PlayerController playerController; // 新增玩家控制器引用
    private Vector2 mouseStartPosition;
    private bool isDragging = false;

    private void Start()
    {
        //O󪺭lYm
        originalY1 = object1.transform.position.y;
        originalY2 = object2.transform.position.y;
         // 獲取玩家控制器引用
        playerController = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        // 檢查是否正在播放動畫或玩家正在移動
        if (isAnimating || (playerController != null && playerController.walking))
        {
            // 如果玩家開始移動，取消當前的滑動操作
            if (isDragging)
            {
                isDragging = false;
            }
            return;
        }

        // 當按下滑鼠時記錄起始位置
        if (Input.GetMouseButtonDown(0))
        {
            mouseStartPosition = Input.mousePosition;
            isDragging = true;
        }

        // 當放開滑鼠時計算滑動距離和方向
        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            isDragging = false;
            Vector2 swipeDelta = (Vector2)Input.mousePosition - mouseStartPosition;

            // 只有當垂直滑動距離超過最小值時才觸發
            if (Mathf.Abs(swipeDelta.y) >= minSwipeDistance)
            {
                isAnimating = true;
                if (swipeDelta.y > 0) // 向上滑動
                {
                    MoveObject(object1, Mathf.RoundToInt(height1));
                    MoveObject(object2, Mathf.RoundToInt(height2));
                }
                else // 向下滑動
                {
                    MoveObject(object1, -Mathf.RoundToInt(height1));
                    MoveObject(object2, -Mathf.RoundToInt(height2));
                }
            }
        }
    }

    private void MoveObject(GameObject obj, int height)
    {
        float newY;

        if (height > 0)
        {
            newY = originalY1 + height;
            if (obj == object2) newY = originalY2 + height;
        }
        else
        {
            newY = obj == object1 ? originalY1 : originalY2;
        }

        if (newY >= originalY1 && obj == object1 || newY >= originalY2 && obj == object2)
        {
            // 禁用玩家移動
            if (playerController != null)
                playerController.SetPlayerMovement(false);

            // 使用 OnComplete 回調在動畫完成後恢復玩家移動
            obj.transform.DOMoveY(newY, duration)
                .OnComplete(() => {
                    if (playerController != null)
                        playerController.SetPlayerMovement(true);
                    // 只有在最後一個物體完成動畫時才重置動畫狀態
                    if (obj == object2)
                    {
                        isAnimating = false;
                    }
                });
        }
    }
}
using UnityEngine;

public class ButtonHandler : MonoBehaviour//遊戲結束顯示字體
{
    public Walkable targetWalkable; // 指定要改變的 Walkable 物件
    public bool isPressed = false; // 公開的按鈕狀態變數

    private bool previousPressState = false; // 用來追蹤按鈕狀態的變化

    private void Update()
    {
        // 檢查按鈕狀態是否發生改變
        if (isPressed != previousPressState)
        {
            // 當按鈕被按下時（狀態從 false 變為 true）
            if (isPressed)
            {
                HandleButtonPress();
            }

            // 更新前一個狀態
            previousPressState = isPressed;
        }
    }

    private void HandleButtonPress()
    {
        if (targetWalkable != null)
        {
            targetWalkable.end = true;
            Debug.Log("The 'end' property of the target Walkable has been set to true.");
        }
        else
        {
            Debug.LogWarning("Target Walkable is not assigned.");
        }
    }

    // 公開方法，可以被其他腳本調用
    public void SetButtonState(bool state)
    {
        isPressed = state;
    }
}
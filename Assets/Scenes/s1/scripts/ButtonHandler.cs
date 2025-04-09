using UnityEngine;

public class ButtonHandler : MonoBehaviour//�C��������ܦr��
{
    public Walkable targetWalkable; // ���w�n���ܪ� Walkable ����
    public bool isPressed = false; // ���}�����s���A�ܼ�

    private bool previousPressState = false; // �ΨӰl�ܫ��s���A���ܤ�

    private void Update()
    {
        // �ˬd���s���A�O�_�o�ͧ���
        if (isPressed != previousPressState)
        {
            // ����s�Q���U�ɡ]���A�q false �ܬ� true�^
            if (isPressed)
            {
                HandleButtonPress();
            }

            // ��s�e�@�Ӫ��A
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

    // ���}��k�A�i�H�Q��L�}���ե�
    public void SetButtonState(bool state)
    {
        isPressed = state;
    }
}
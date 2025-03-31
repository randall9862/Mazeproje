using UnityEngine;

public class ObjectSwitcher
{
    // �x�s���󪺰}�C
    private GameObject[] objects;
    private Relicmagnification Relicmagnification;
    public ObjectSwitcher(GameObject[] objectsToManage)
    {
        objects = objectsToManage;
    }

    // �}�ҩҦ�����
    public void TurnOnAll()
    {
        foreach (GameObject obj in objects)
        {
            if (obj != null)
            {
                obj.SetActive(true);
            }
        }
    }

    // �����Ҧ�����
    public void TurnOffAll()
    {
        foreach (GameObject obj in objects)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }
    }

    // �}�ҫ��w���ު�����
    public void TurnOnAt(int index)
    {
        if (IsValidIndex(index))
        {
            objects[index].SetActive(true);
        }
    }
   
    // �������w���ު�����
    public void TurnOffAt(int index)
    {
        if (IsValidIndex(index))
        {
            objects[index].SetActive(false);
        }
    }

    // �ˬd���ެO�_����
    private bool IsValidIndex(int index)
    {
        return index >= 0 && index < objects.Length;
    }
}
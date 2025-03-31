using UnityEngine;

public class ObjectSwitcher
{
    // 儲存物件的陣列
    private GameObject[] objects;
    private Relicmagnification Relicmagnification;
    public ObjectSwitcher(GameObject[] objectsToManage)
    {
        objects = objectsToManage;
    }

    // 開啟所有物件
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

    // 關閉所有物件
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

    // 開啟指定索引的物件
    public void TurnOnAt(int index)
    {
        if (IsValidIndex(index))
        {
            objects[index].SetActive(true);
        }
    }
   
    // 關閉指定索引的物件
    public void TurnOffAt(int index)
    {
        if (IsValidIndex(index))
        {
            objects[index].SetActive(false);
        }
    }

    // 檢查索引是否有效
    private bool IsValidIndex(int index)
    {
        return index >= 0 && index < objects.Length;
    }
}
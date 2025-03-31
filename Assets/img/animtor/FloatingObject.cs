using UnityEngine;

public class FloatingObject : MonoBehaviour//更換遺物材質
{
    public float floatSpeed = 0.5f; // 上下漂浮速度
    public float floatHeight = 0.2f; // 漂浮高度
    public float rotationSpeed = 30f; // 旋轉速度

    public GameObject[] objects; // 儲存物件陣列
    public Material[] materials; // 儲存材質陣列
    public GameObject[] Relicfragments;//碎片

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // 上下漂浮效果
        float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);

        // 緩慢旋轉效果
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

       // SetObjectMaterial(0,0);
    }

    // 設定物件的材質
    public void SetObjectMaterial(int objectIndex, int materialIndex)
    {
        if (objectIndex >= 0 && objectIndex < objects.Length && materialIndex >= 0 && materialIndex < materials.Length)
        {
            Renderer renderer = objects[objectIndex].GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material = materials[materialIndex];
            }
        }
    }
    public void DisableRelicsred()
    {
        Relicfragments[2].SetActive(false);
    }
    public void DisableRelicsgreen()
    {
        Relicfragments[1].SetActive(false);
    }
    public void DisableRelicsbiue()
    {
        Relicfragments[0].SetActive(false);
    }
}

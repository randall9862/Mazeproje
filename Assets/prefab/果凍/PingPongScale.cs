using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//--------------------------------- PingPongScale -----------------------------------
public class PingPongScale : MonoBehaviour
{
    public float duration = 2;
    public float scaleX = 1, scaleY = 1;
    float originalScaleX, originalScaleY, originalScaleZ;

    void Start()
    {
        originalScaleX = transform.localScale.x;
        originalScaleY = transform.localScale.y;
        originalScaleZ = transform.localScale.z;
    }

    void Update()
    {
        if (scaleX + Time.deltaTime / duration > 1)
        {
            scaleX = 1;
            scaleY = 0;
            duration *= -1;
        }
        else if (scaleX + Time.deltaTime / duration < 0)
        {
            scaleX = 0;
            scaleY = 1;
            duration *= -1;
        }
        else
        {
            scaleX += Time.deltaTime / duration;
            scaleY = 1 - scaleX;
        }

        transform.localScale = new Vector3(originalScaleX * scaleX,
                                           originalScaleY * scaleY,
                                           originalScaleZ);
    }
}



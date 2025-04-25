using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBackgroundEffect2 : MonoBehaviour
{
    public Material backgroundMat; // 背景 Shader Material
    public GameObject cameraObject; // 指定相機的 GameObject
    private Material instanceMat;   // 獨立的 Material 實例

    private void OnEnable()
    {
        // 創建獨立的 Material 實例
        if (backgroundMat != null)
        {
            instanceMat = new Material(backgroundMat);
        }

        // 如果指定了 cameraObject，確保這個腳本附加到該相機上
        if (cameraObject != null && cameraObject.GetComponent<Camera>() != null)
        {
            if (this.gameObject != cameraObject)
            {
                Debug.LogWarning("This script should be attached to the specified camera object!");
            }
        }
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (instanceMat != null)
        {
            Graphics.Blit(src, dest, instanceMat); // 使用獨立的 Material 渲染
        }
        else
        {
            Graphics.Blit(src, dest); // 如果沒有 Material，保持原始輸出
        }
    }

    private void OnDisable()
    {
        // 清理實例化的 Material，避免內存洩漏
        if (instanceMat != null)
        {
            DestroyImmediate(instanceMat);
        }
    }
}

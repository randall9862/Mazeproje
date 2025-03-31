using UnityEngine;

[ExecuteInEditMode]
public class CameraBackgroundEffect : MonoBehaviour
{
    public Material backgroundMat; // 指定 Shader 用的材質

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (backgroundMat != null)
        {
            Graphics.Blit(src, dest, backgroundMat); // 用 Shader 改變攝影機輸出
        }
        else
        {
            Graphics.Blit(src, dest); // 如果沒設定材質，直接顯示原始畫面
        }
    }
}

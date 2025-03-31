using UnityEngine;

[ExecuteInEditMode]
public class CameraBackgroundEffect : MonoBehaviour
{
    public Material backgroundMat; // ���w Shader �Ϊ�����

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (backgroundMat != null)
        {
            Graphics.Blit(src, dest, backgroundMat); // �� Shader ������v����X
        }
        else
        {
            Graphics.Blit(src, dest); // �p�G�S�]�w����A������ܭ�l�e��
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PostProcessing : MonoBehaviour
{
    public Material mat;
    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
        cam.depthTextureMode = DepthTextureMode.DepthNormals;
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest, mat);
    }
   // [��ó] ����Ƽ�� �̿��� ������ ȣ�� ��ȯ ������ ����Ʈ���μ��� �������� -2-|�ۼ��� ����

}
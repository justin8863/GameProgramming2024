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
   // [출처] 유니티를 이용한 오브라딘 호의 귀환 느낌의 포스트프로세스 구현일지 -2-|작성자 턍찌

}
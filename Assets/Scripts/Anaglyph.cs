using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Anaglyph : MonoBehaviour
{
    public Shader fxShader;
    public Camera cam2;
    public float stereoWidth = 1.0f;

    private Material mat;
    private RenderTexture rt;

    private void Start()
    {
        transform.localEulerAngles = Vector3.up * stereoWidth;
        cam2.transform.localEulerAngles = Vector3.up * -stereoWidth;
    }


    private void OnEnable()
    {
        if(fxShader== null) { enabled = false; return; }
        mat = new Material(fxShader);
        mat.hideFlags = HideFlags.HideAndDontSave;
        cam2.enabled = false;
        int w = Screen.width, h = Screen.height;
        rt = new RenderTexture(w, h, 8, RenderTextureFormat.Default);
        cam2.targetTexture = rt;
    }

    private void OnDisable()
    {
        if (mat != null) { DestroyImmediate(mat); }
        if (rt != null) { rt.Release(); }
        cam2.targetTexture = null;

    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if(cam2 == null || mat == null || rt == null) { enabled = false; return; }
        cam2.Render();
        mat.SetTexture("_MainTex2", rt);
        Graphics.Blit(source,destination,mat);
        rt.Release();
    }
}

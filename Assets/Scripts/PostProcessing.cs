using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostProcessing : MonoBehaviour
{
    public Material mat;
    public float brightness = 1.0f;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        //Debug.Log(brightness);
        mat.SetFloat("_Brightness", 1.2f);
        Graphics.Blit(source, destination, mat);
    }

    public void SetBrightness(float brightness)
    {
        Debug.Log("QWE");
        this.brightness = brightness;
        Debug.Log(this.brightness);
    }
}

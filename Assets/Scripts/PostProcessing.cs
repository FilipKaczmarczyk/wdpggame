using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostProcessing : MonoBehaviour
{
    public Material mat;
    private float brightness = 1.0f;
    private float contrast = 1.0f;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        mat.SetFloat("_Brightness", brightness);
        Graphics.Blit(source, destination, mat);
    }

    public void SetBrightness(float brightness)
    {
        this.brightness = brightness;
    }

    public void SetContrast(float contrast)
    {
        this.contrast = contrast;
    }
}

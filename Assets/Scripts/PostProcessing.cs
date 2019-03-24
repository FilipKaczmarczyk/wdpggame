using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostProcessing : MonoBehaviour
{
    public Material mat;
    private static float brightness = 1.0f;
    private static float contrast = 1.0f;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        mat.SetFloat("_Brightness", brightness);
        mat.SetFloat("_Contrast", contrast);
        Graphics.Blit(source, destination, mat);
    }

    public void SetBrightness(float brightness)
    {
        PostProcessing.brightness = brightness;
    }

    public void SetContrast(float contrast)
    {
        PostProcessing.contrast = contrast;
    }

    public void SetDefaults()
    {
        PostProcessing.brightness = 1f;
        PostProcessing.contrast = 1f;
    }
}

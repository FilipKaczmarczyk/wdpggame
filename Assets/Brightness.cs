using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brightness : MonoBehaviour
{
	[Range(0.5f, 1.5f)] public float _Brightness = 1f;    
	[Range(0.5f, 1.5f)] public float _Contrast = 1f;

	
	public void SetBrightness(float value)
	{
		_Brightness = value;
		
		Material material = GetComponent<MeshRenderer>().material;
		material.SetFloat("_Brightness", _Brightness)
	}
	public void SetContrast(float value)
	{
		_Contrast = value;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0649

public class SwordCollider : MonoBehaviour
{
	[SerializeField]
	private string targetTag;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == targetTag)
		{
			GetComponent<Collider2D>().enabled = false;
		}
	}
}

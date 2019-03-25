using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0649

public class EnemySight : MonoBehaviour
{
	[SerializeField]
	private Enemy enemy;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Player")
		{
			enemy.Target = other.gameObject;
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			enemy.Target = null;
		}
	}

}

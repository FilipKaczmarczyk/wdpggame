using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
	private SpriteRenderer spriteRenderer2;

	public void Start()
	{
		spriteRenderer2 = GetComponent<SpriteRenderer>();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Sword" || other.gameObject.tag == "Fireball")
		{
			StartCoroutine(IndicateImmoratalX());
		}
	}

	private IEnumerator IndicateImmoratalX()
	{
			spriteRenderer2.enabled = false;
			yield return new WaitForSeconds(.05f);
			spriteRenderer2.enabled = true;
			yield return new WaitForSeconds(.05f);
	}
}

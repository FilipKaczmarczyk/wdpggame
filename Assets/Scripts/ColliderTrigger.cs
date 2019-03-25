using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0649

public class ColliderTrigger : MonoBehaviour
{
	[SerializeField]
	private BoxCollider2D platformCollider;

	[SerializeField]
	private BoxCollider2D platformTrigger;

    void Start()
    {
		Physics2D.IgnoreCollision(platformCollider, platformTrigger, true);
    }

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")
		{
			Physics2D.IgnoreCollision(platformCollider, other, true);
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")
		{
			Physics2D.IgnoreCollision(platformCollider, other, false);
		}
	}

}

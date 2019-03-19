﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0649

public class ColliderTrigger : MonoBehaviour
{

	private BoxCollider2D playerCollider;

	[SerializeField]
	private BoxCollider2D platformCollider;

	[SerializeField]
	private BoxCollider2D platformTrigger;

    void Start()
    {
		playerCollider = GameObject.Find("Player").GetComponent<BoxCollider2D>();
		Physics2D.IgnoreCollision(platformCollider, platformTrigger, true);
    }

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.name == "Player")
		{
			Physics2D.IgnoreCollision(platformCollider, playerCollider, true);
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.name == "Player")
		{
			Physics2D.IgnoreCollision(platformCollider, playerCollider, false);
		}
	}

}

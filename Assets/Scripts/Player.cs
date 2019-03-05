using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	private Rigidbody2D playerRigidbody;

	private Animator myAnimator;

	[SerializeField]
	private float movementSpeed = 10f;

	private bool directionRight;


    // Start is called before the first frame update
    void Start()
    {
		directionRight = true;
		playerRigidbody = GetComponent<Rigidbody2D>();
		myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		float horizontal = Input.GetAxis("Horizontal");
		HandleMovement(horizontal);
		Flip(horizontal);
    }

	private void HandleMovement(float horizontal)
	{
		playerRigidbody.velocity = new Vector2(horizontal * movementSpeed, playerRigidbody.velocity.y);
		myAnimator.SetFloat("Player_speed", Mathf.Abs(horizontal));
	}

	private void Flip(float horizontal)
	{
		if((horizontal > 0 && !directionRight) || (horizontal < 0 && directionRight))
		{
			directionRight = !directionRight;
			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
		}
	}
}

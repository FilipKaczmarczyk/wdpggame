using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	private Rigidbody2D playerRigidbody;

	private Animator playerAnimator;

	[SerializeField]
	private float movementSpeed = 10;

	private bool attack;

	private bool slide;

	private bool directionRight;

	[SerializeField]
	private Transform[] groundPoints;

	[SerializeField]
	private float groundRadius;

	[SerializeField]
	private LayerMask WhatIsGround;

	private bool isGrounded;

	private bool jump;

	[SerializeField]
	private bool airControl;

	[SerializeField]
	private float jumpForce = 400;
	
    void Start()
    {
		directionRight = true;
		playerRigidbody = GetComponent<Rigidbody2D>();
		playerAnimator = GetComponent<Animator>();
    }

	void Update()
	{ 
		HandleInput();
	}

	void FixedUpdate()
    {
		float horizontal = Input.GetAxis("Horizontal");
		isGrounded = IsGrounded();
		HandleMovement(horizontal);
		Flip(horizontal);
		HandleAttacks();
		HandleLayers();
		ResetValues();

    }

	private void HandleMovement(float horizontal)
	{
		if (playerRigidbody.velocity.y < 0)
		{
			playerAnimator.SetBool("Player_land", true);
		}
		if (!playerAnimator.GetBool("Player_slide") && !this.playerAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Player_attack") && (isGrounded || airControl))
		{
			playerRigidbody.velocity = new Vector2(horizontal * movementSpeed, playerRigidbody.velocity.y);
		}
		if (isGrounded && jump)
		{
			isGrounded = false;
			playerRigidbody.AddForce(new Vector2(0, jumpForce));
			playerAnimator.SetTrigger("Player_jump");
		}

		playerAnimator.SetFloat("Player_speed", Mathf.Abs(horizontal));

		if (slide && !this.playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Player_slide"))
		{
			playerAnimator.SetBool("Player_slide", true);
		}
		else if (!this.playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Player_slide"))
		{
			playerAnimator.SetBool("Player_slide", false);
		}
	}

	private void HandleAttacks()
	{
		if(attack && !this.playerAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Player_attack") && !this.playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Player_slide"))
		{
			playerAnimator.SetTrigger("Player_attack");
			playerRigidbody.velocity = Vector2.zero;
		}
	}

	private void HandleInput()
	{
		if(Input.GetKeyDown(KeyCode.A))
		{
			attack = true;
		}

		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			slide = true;
		}

		if (Input.GetKeyDown(KeyCode.S))
		{
			jump = true;
		}
	}

	private void Flip(float horizontal)
	{
		if((horizontal > 0 && !directionRight) || (horizontal < 0 && directionRight))
		{
			if(!this.playerAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Player_attack") && !this.playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Player_slide"))
			{
				directionRight = !directionRight;
				Vector3 theScale = transform.localScale;
				theScale.x *= -1;
				transform.localScale = theScale;
			}
		}
	}

	private void ResetValues()
	{
		attack = false;
		slide = false;
		jump = false;
	}

	private bool IsGrounded()
	{
		if (playerRigidbody.velocity.y <= 0)
		{
			foreach (Transform point in groundPoints)
			{
				Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, WhatIsGround);

				for (int i = 0; i < colliders.Length; i++)
				{
					if (colliders[i].gameObject != gameObject)
					{
						playerAnimator.ResetTrigger("Player_jump");
						playerAnimator.SetBool("Player_land", false);
						return true;
					}
				}
			}
		}
		return false;
	}

	private void HandleLayers()
	{
		if (!isGrounded)
		{
			playerAnimator.SetLayerWeight(1, 1);
		}
		else
		{
			playerAnimator.SetLayerWeight(1, 0);
		}
	}
}

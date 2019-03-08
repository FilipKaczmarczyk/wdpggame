using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	private Rigidbody2D playerRigidbody;

	private Animator playerAnimator;

	[SerializeField]
	private float movementSpeed = 10f;

	private bool attack;

	private bool slide;

	private bool directionRight;


    // Start is called before the first frame update
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
		HandleMovement(horizontal);
		Flip(horizontal);
		HandleAttacks();
		ResetValues();

    }

	private void HandleMovement(float horizontal)
	{
		if (!playerAnimator.GetBool("Player_slide") && !this.playerAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Player_attack"))
		{
			playerRigidbody.velocity = new Vector2(horizontal * movementSpeed, playerRigidbody.velocity.y);
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
	}
}

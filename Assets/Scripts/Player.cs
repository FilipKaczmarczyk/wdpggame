using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0649

public class Player : MonoBehaviour
{
	private static Player instance;

	public static Player Instance
	{
		get
		{
			if (instance == null)
			{
				instance = GameObject.FindObjectOfType<Player>();
			}
			return instance;
		}
	}

	private Animator playerAnimator;

	[SerializeField]
	private Transform fireballPosition;

	[SerializeField]
	private float movementSpeed = 10;

	private bool directionRight;

	[SerializeField]
	private Transform[] groundPoints;

	[SerializeField]
	private float groundRadius = 0.2f;

	[SerializeField]
	private LayerMask WhatIsGround;

	[SerializeField]
	private bool airControl = true;

	[SerializeField]
	private float jumpForce = 500;

	[SerializeField]
	private GameObject firePrefab;

	public Rigidbody2D PlayerRigibody { get; set; }

	public bool Attack { get; set; }

	public bool Slide { get; set; }

	public bool Jump { get; set; }

	public bool OnGround { get; set; }

	void Start()
    {
		directionRight = true;
		PlayerRigibody = GetComponent<Rigidbody2D>();
		playerAnimator = GetComponent<Animator>();
    }

	void Update()
	{ 
		HandleInput();
	}

	void FixedUpdate()
    {
		float horizontal = Input.GetAxis("Horizontal");
		OnGround = IsGrounded();
		HandleMovement(horizontal);
		Flip(horizontal);
		HandleLayers();
    }

	private void HandleMovement(float horizontal)
	{
		if (PlayerRigibody.velocity.y < 0)
		{
			playerAnimator.SetBool("Player_land", true);
		}
		if ((!Attack && !Slide && (OnGround || airControl)))
		{
			PlayerRigibody.velocity = new Vector2(horizontal * movementSpeed, PlayerRigibody.velocity.y);
		}
		if (Jump && PlayerRigibody.velocity.y == 0)
		{
			PlayerRigibody.AddForce(new Vector2(0, jumpForce));
		}

		playerAnimator.SetFloat("Player_speed", Mathf.Abs(horizontal));
	}

	private void HandleInput()
	{
		if(Input.GetKeyDown(KeyCode.A))
		{
			playerAnimator.SetTrigger("Player_attack");
		}

		if (Input.GetKeyDown(KeyCode.LeftShift) && OnGround)
		{
			playerAnimator.SetTrigger("Player_slide");
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
			playerAnimator.SetTrigger("Player_jump");
		}

		if (Input.GetKeyDown(KeyCode.S))
		{
			playerAnimator.SetTrigger("Player_skill");
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

	private bool IsGrounded()
	{
		if (PlayerRigibody.velocity.y <= 0)
		{
			foreach (Transform point in groundPoints)
			{
				Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, WhatIsGround);

				for (int i = 0; i < colliders.Length; i++)
				{
					if (colliders[i].gameObject != gameObject)
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	private void HandleLayers()
	{
		if (!OnGround)
		{
			playerAnimator.SetLayerWeight(1, 1);
		}
		else
		{
			playerAnimator.SetLayerWeight(1, 0);
		}
	}

	public void SpellCast(int value)
	{
		if ((!OnGround && value == 1) || (OnGround && value == 0))
		{
			if (directionRight)
			{
				GameObject tmp = (GameObject)Instantiate(firePrefab, fireballPosition.position, Quaternion.identity);
				tmp.GetComponent<Fireball>().Initialize(Vector2.right);
			}
			else
			{
				GameObject tmp = (GameObject)Instantiate(firePrefab, fireballPosition.position, Quaternion.Euler(new Vector3(0, 0, 180)));
				tmp.GetComponent<Fireball>().Initialize(Vector2.left);
			}
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#pragma warning disable 0649

public delegate void DeadEventHandler();

public class Player : Character
{
	private static Player instance;

	public event DeadEventHandler Dead;

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

	private bool immortal = false;

	private SpriteRenderer spriteRenderer;

	[SerializeField]
	private float immortalTime;

	public Rigidbody2D PlayerRigibody { get; set; }

	public bool Slide { get; set; }

	public bool Jump { get; set; }

	public bool OnGround { get; set; }

	public override bool IsDead
	{
		get
		{
			if(health <= 0)
			{
				OnDead();
			}

			return health <= 0;
		}

	}


	private Vector2 startPos;

	public override void Start()
    {
		base.Start();
		startPos = transform.position;

		spriteRenderer = GetComponent<SpriteRenderer>();
		PlayerRigibody = GetComponent<Rigidbody2D>();
    }

	void Update()
	{ 
		if(!TakingDamage && !IsDead)
		{
			if (transform.position.y <= -14f)
			{
				Death();
			}
			HandleInput();
		}
		
	}

	void FixedUpdate()
    {
		if (!TakingDamage && !IsDead)
		{
			float horizontal = Input.GetAxis("Horizontal");
			OnGround = IsGrounded();
			HandleMovement(horizontal);
			Flip(horizontal);
			HandleLayers();
		}
    }

	public void OnDead()
	{
		if (Dead != null)
		{
			Dead();
		}
	}

	private void HandleMovement(float horizontal)
	{
		if (PlayerRigibody.velocity.y < 0)
		{
			MyAnimator.SetBool("land", true);
		}
		if ((!Attack && !Slide && (OnGround || airControl)))
		{
			PlayerRigibody.velocity = new Vector2(horizontal * movementSpeed, PlayerRigibody.velocity.y);
		}
		if (Jump && PlayerRigibody.velocity.y == 0)
		{
			PlayerRigibody.AddForce(new Vector2(0, jumpForce));
		}

		MyAnimator.SetFloat("speed", Mathf.Abs(horizontal));
	}

	private void HandleInput()
	{
		if(Input.GetKeyDown(KeyCode.A))
		{
			MyAnimator.SetTrigger("attack");
		}

		if (Input.GetKeyDown(KeyCode.LeftShift) && OnGround)
		{
			MyAnimator.SetTrigger("slide");
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
			MyAnimator.SetTrigger("jump");
		}

		if (Input.GetKeyDown(KeyCode.S))
		{
			MyAnimator.SetTrigger("skill");
		}

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0); // Main Menu
        }
    }

	private void Flip(float horizontal)
	{
		if((horizontal > 0 && !directionRight) || (horizontal < 0 && directionRight))
		{
			if(!Attack && !Slide)
			{
				ChangeDirection();
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
			MyAnimator.SetLayerWeight(1, 1);
		}
		else
		{
			MyAnimator.SetLayerWeight(1, 0);
		}
	}

	public override void ThrowWeapon(int value)
	{
		if ((!OnGround && value == 1) || (OnGround && value == 0))
		{
			base.ThrowWeapon(value);
		}
	}

	private IEnumerator IndicateImmoratal()
	{
		while (immortal)
		{
			spriteRenderer.enabled = false;
			yield return new WaitForSeconds(.05f);
			spriteRenderer.enabled = true;
			yield return new WaitForSeconds(.05f);
		}
	}

	public override IEnumerator TakeDamage()
	{
		if (!immortal)
		{
			health -= 20;
			if (!IsDead)
			{
				MyAnimator.SetTrigger("damage");
				immortal = true;
				StartCoroutine(IndicateImmoratal());

				yield return new WaitForSeconds(immortalTime);
				immortal = false;
			}
			else
			{
				MyAnimator.SetLayerWeight(1, 0);
				MyAnimator.SetTrigger("die");
			}

			yield return null;
		}
	}

	public override void Death()
	{
		PlayerRigibody.velocity = Vector2.zero;
		MyAnimator.SetTrigger("idle");
		health = 100;
		transform.position = startPos;
	}
}

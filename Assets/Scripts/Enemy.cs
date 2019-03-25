using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0649

public class Enemy : Character
{

	private IEnemyState currentState;

	[SerializeField]
	protected Transform soulPosition;

	[SerializeField]
	private GameObject soulPrefab;

	public GameObject Target { get; set; }

	[SerializeField]
	private float meleeRange;

	[SerializeField]
	private float throwRange = 12;

	[SerializeField]
	private Transform leftEdge;

	[SerializeField]
	private Transform rightEdge;

	public bool InMeleeRange
	{
		get
		{
			if (Target != null)
			{
				return Vector2.Distance(transform.position, Target.transform.position) <= meleeRange;
			}

			return false;
		}
	
	}

	public bool InThrowRange
	{
		get
		{
			if (Target != null)
			{
				return Vector2.Distance(transform.position, Target.transform.position) <= throwRange;
			}

			return false;
		}

	}

	public override bool IsDead
	{
		get
		{
			return health <= 0;
		}
	}

	// Start is called before the first frame update
	public override void Start()
    {
		base.Start();
		Player.Instance.Dead += new DeadEventHandler(RemoveTarget);
		ChangeState(new IdleState());
    }

    // Update is called once per frame
    void Update()
    {
		if (!IsDead)
		{
			if (!TakingDamage)
			{
				currentState.Execute();
			}
			LookAtTarget();
		}
    }

	private void RemoveTarget()
	{
		Target = null;
		ChangeState(new PatrolState());
	}

	private void LookAtTarget()
	{
		if (Target != null)
		{
			float xDirection = Target.transform.position.x - transform.position.x;
			if (xDirection < 0 && directionRight || xDirection > 0 && !directionRight)
			{
				ChangeDirection();
			}
		}
	}

	public void ChangeState(IEnemyState newState)
	{
		if(currentState != null)
		{
			currentState.Exit();
		}

		currentState = newState;

		currentState.Enter(this);
	}

	public void Move()
	{
		if (!Attack)
		{
			if ((GetDirection().x > 0 && transform.position.x < rightEdge.position.x) || (GetDirection().x < 0 && transform.position.x > leftEdge.position.x))
			{
				MyAnimator.SetFloat("speed", 1);
				transform.Translate(GetDirection() * (movementSpeed * Time.deltaTime));
			}
			else if (currentState is PatrolState)
			{
				ChangeDirection();
			}
		}
	}

	public Vector2 GetDirection()
	{
		return directionRight ? Vector2.right : Vector2.left;
	}

	public override void OnTriggerEnter2D(Collider2D other)
	{
		base.OnTriggerEnter2D(other);
		currentState.OnTriggerEnter(other);
	}

	public override IEnumerator TakeDamage()
	{
		health -= 20;
		if (!IsDead)
		{
			MyAnimator.SetTrigger("damage");

		} else
		{
			MyAnimator.SetTrigger("die");
			yield return null;
		}
	}

	public override void Death()
	{
		Destroy(gameObject);
		spawnSoul();
	}

	public void spawnSoul()
	{
		GameObject soul = (GameObject)Instantiate(soulPrefab, soulPosition.position, Quaternion.identity);
	}
}

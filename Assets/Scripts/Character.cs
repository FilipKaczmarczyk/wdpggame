using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0649

public abstract class Character : MonoBehaviour
{
	[SerializeField]
	protected Transform weaponPosition;

	[SerializeField]
	protected float movementSpeed = 10;

	protected bool directionRight;

	[SerializeField]
	private GameObject weaponPrefab;

	[SerializeField]
	protected int health = 100;

	[SerializeField]
	private EdgeCollider2D mainWeaponCollider;

	[SerializeField]
	private List<string> damageSources;

	public abstract bool IsDead { get; }

	public bool Attack { get; set; }

	public bool TakingDamage { get; set; }

	public Animator MyAnimator { get; private set; }

	public EdgeCollider2D MainWeaponCollider
	{
		get
		{
			return mainWeaponCollider;
		}
	}

	// Start is called before the first frame update
	public virtual void Start()
    {
		directionRight = true;
		MyAnimator = GetComponent<Animator>();
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	public abstract IEnumerator TakeDamage();

	public abstract void Death();

	public void ChangeDirection()
	{
		directionRight = !directionRight;
		transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y * 1, 1);
	}

	public virtual void ThrowWeapon(int value)
	{
		if (directionRight)
		{
			GameObject tmp = (GameObject)Instantiate(weaponPrefab, weaponPosition.position, Quaternion.identity);
			tmp.GetComponent<Weapon>().Initialize(Vector2.right);
		}
		else
		{
			GameObject tmp = (GameObject)Instantiate(weaponPrefab, weaponPosition.position, Quaternion.Euler(new Vector3(0, 0, 180)));
			tmp.GetComponent<Weapon>().Initialize(Vector2.left);
		}
	}

	public void MeleeAttack()
	{
		MainWeaponCollider.enabled = true;
	}

	public virtual void OnTriggerEnter2D(Collider2D other)
	{
	
		if (damageSources.Contains(other.tag))
		{
			StartCoroutine(TakeDamage());
		}

		if (other.gameObject.tag == "Soul")
		{
			GameManager.Instance.CollectedSouls++;
			Destroy(other.gameObject);
		}

	}
}

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

	public bool Attack { get; set; }

	public Animator MyAnimator { get; private set; }

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
}

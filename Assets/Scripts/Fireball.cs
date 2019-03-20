using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Fireball : MonoBehaviour
{
	[SerializeField]
	private float speed = 10;

	private Rigidbody2D fireRigibody;

	private Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
		fireRigibody = GetComponent<Rigidbody2D>();
    }

	void FixedUpdate()
	{
		fireRigibody.velocity = direction * speed;
	}

	// Update is called once per frame
	void Update()
    {
        
    }

	public void Initialize(Vector2 direction)
	{
		this.direction = direction;
	}

	private void OnBecameInvisible()
	{
		Destroy(gameObject);
	}
}

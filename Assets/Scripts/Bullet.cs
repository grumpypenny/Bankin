using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Bullet : MonoBehaviour
{
	public float lifeTime = 10f;

	private float speed;
	private float damage;
	private Vector2 direction;
	[SerializeField]
	private LayerMask hitMask;
	private float rad;
	private bool isSetUp = false;

	private void Awake()
	{
		rad = GetComponent<CircleCollider2D>().radius;
		// call set up when this object is fired
		PlayerShooting.instance.OnPlayerFire += SetVariables;

		// destroy after lifetime seconds if it doesn't hit anything
		Destroy(gameObject, lifeTime);
	}

    public void SetVariables(float _speed, float _damage, Vector2 _direction, LayerMask _hitMask)
	{
		if (!isSetUp)
		{
			speed = _speed;
			damage = _damage;
			direction = _direction;
			isSetUp = true;
			hitMask = _hitMask;
			isSetUp = true;
			PlayerShooting.instance.OnPlayerFire -= SetVariables;
		}

	}

	private void Update()
	{
		if (isSetUp)
		{
			transform.position += (Vector3)direction * speed * Time.deltaTime;
		}
		Collided(hitMask);
	}

	public void Collided(LayerMask hitMask)
	{
		//Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, rad, hitMask);
		Collider2D other = Physics2D.OverlapCircle(transform.position, rad, hitMask);
		if (other != null)
		{
			print("hit " + other.name);
			Destroy(gameObject);
		}
	}
}

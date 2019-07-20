﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// NOTE: Need to add event for each kind of shooting script
[RequireComponent(typeof(CircleCollider2D))]
public class Bullet : MonoBehaviour
{
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
	}

	// called by event only, sets up bullet to match gun
    public void SetVariables(float _speed, float _damage, Vector2 _direction, LayerMask _hitMask, float _lifeTime)
	{
		if (!isSetUp)
		{
			speed = _speed;
			damage = _damage;
			direction = _direction;
			isSetUp = true;
			hitMask = _hitMask;
			isSetUp = true;

			// unsubscibe from player events
			PlayerShooting.instance.OnPlayerFire -= SetVariables;

			// destroy after lifetime seconds if it doesn't hit anything
			Destroy(gameObject, _lifeTime);
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

	// check if bullet has collided with anything
	public void Collided(LayerMask hitMask)
	{
		Collider2D other = Physics2D.OverlapCircle(transform.position, rad, hitMask);
		if (other != null)
		{
			print("hit " + other.name);

			HealthData otherHealth = other.GetComponent<HealthInstance>().health;

			if (otherHealth != null)
			{
				otherHealth.TakeDamage(damage);
			}

			Destroy(gameObject);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Health Data", menuName = "Health/Health Data")]
public class HealthData : ScriptableObject
{
	public int maxHealth;
	[Range(0,1)]
	public float damageReduction;

	[SerializeField] private float currentHealth;
	[SerializeField] private bool isDead = false;

	private void OnEnable()
	{
		currentHealth = maxHealth;
		isDead = false;
	}

	public void TakeDamage(float amount)
	{
		currentHealth -= amount * (1 - damageReduction);
		if (currentHealth <= 0)
		{
			Die();
		}
	}

	public bool GetState()
	{
		return isDead;
	}

	private void Die()
	{
		isDead = true;
	}
}

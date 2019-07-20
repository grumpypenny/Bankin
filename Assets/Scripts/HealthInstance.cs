using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// manage health
public class HealthInstance : MonoBehaviour
{
	public HealthData health;

	private void Update()
	{
		if (health != null && health.GetState())
		{
			gameObject.SetActive(false);
		}
	}
}

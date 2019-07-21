using System;
using System.Collections.Generic;
using UnityEngine;

/*
 * This class is meant to trigger the bullet event whenever a guard shoots
 */ 

public class AIShootingManager : MonoBehaviour
{
	#region Singleton
	public static AIShootingManager instance;

	private void Awake()
	{
		if (instance != null)
		{
			Debug.LogWarning("Multiple Instances of AIShootingManager");
			return;
		}
		instance = this;
	}

	#endregion

	[Header("Asset")]
	public GameObject asset;

	public event Action<float, float, Vector2, LayerMask, float> OnAIFire;

	public void SendFireEvent(Transform _transform, float _speed, float _damage,
		Vector2 _direction, LayerMask _hitMask, float _lifeTime)
	{
		Instantiate(asset, _transform.position, transform.rotation);

		if (OnAIFire != null)
		{
			OnAIFire.Invoke(_speed, _damage, _direction, _hitMask, _lifeTime);
		}
	}
}

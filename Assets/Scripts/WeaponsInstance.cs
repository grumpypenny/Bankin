using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsInstance : MonoBehaviour
{
	[Header("The ITEM")]
	public Weapon item;
	public float pickUpRadius = 2f;

	[Space]

	[Header("Pick Up Button")]
	public KeyCode key;

	private Vector3 playerPosition;

	private void Update()
	{
		playerPosition = PlayerInventory.instance.transform.position;

		if (CanPickUp(playerPosition) && Input.GetKeyDown(key))
		{
			PlayerInventory.instance.AddWeapon(item);
			gameObject.SetActive(false);
		}
	}

	// can this item be picked up by player
	private bool CanPickUp(Vector3 otherPosition)
	{
		if (Vector3.Distance(transform.position, otherPosition) <= pickUpRadius)
		{
			return true;
		}
		return false;
	}
}

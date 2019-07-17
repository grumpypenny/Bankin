using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This class is for the IN GAME object for the player to interact with
 */ 

public class ItemPickUp : MonoBehaviour
{
	[Header("The ITEM")]
	public PickUp item;
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
			if (PlayerInventory.instance.AddPickUp(item))
			{
				gameObject.SetActive(false);
			}
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

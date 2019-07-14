using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * NOTE: Player Can only use one weapon at a time
 * Gun is changed via menu
 */ 


public class PlayerInventory : MonoBehaviour
{
	#region Singleton
	public static PlayerInventory instance;

	private void Awake()
	{
		if (instance != null)
		{
			Debug.LogWarning("Multiple Instances of PlayerInventory");
			return;
		}
		instance = this;
	}

	#endregion


	[Header("Items")]
	public List<Weapon> weapons = new List<Weapon>();
	public List<PickUp> pickUps = new List<PickUp>();

	[Space]

	[Header("Inventory Properties")]
	[Range(1, 5)]
	public int maxPickUps;

	[Space]

	[Header("Current State")]
	public Weapon currentWeapon = null;


	public void ChangeWeapon(Weapon newWeapon)
	{
		if (weapons.Count > 0)
		{
			weapons.Clear();
		}
		weapons.Add(newWeapon);
	}

	public bool AddPickUp(PickUp newPickUp)
	{
		if (pickUps.Count <= maxPickUps)
		{
			Debug.Log("picked up " + newPickUp.name);
			pickUps.Add(newPickUp);
			return true;
		}
		else
		{
			Debug.Log("Can not hold more items");
			return false;
		}
	}

	// Drop off all the pick ups (Money) player has picked up
	public List<PickUp> DropAllPickUps()
	{
		List<PickUp> output = new List<PickUp>();
		foreach (PickUp item in pickUps)
		{
			output.Add(item);
			pickUps.Remove(item);
		}
		return output;
	}
}

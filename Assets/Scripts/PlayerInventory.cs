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
	private int currentIndex = 0;

	public delegate void NewCurrentWeapon();
	public event NewCurrentWeapon weaponChange;

	public void AddWeapon(Weapon newWeapon)
	{
		weapons.Add(newWeapon);
		if (currentWeapon == null)
		{
			currentWeapon = newWeapon;
			if (newWeapon != null)
			{
				weaponChange();
			}
		}
	}

	public void DropWeapon()
	{
		weapons.Remove(currentWeapon);
	}

	public void ChangeWeapon()
	{
		weaponChange?.Invoke();

		if (currentWeapon == null && weapons.Count > 0)
		{
			// eqip first weapon
			currentWeapon = weapons[0];
			currentIndex = 0;
			return;
		}
		// change current weapon to next index
		currentIndex++;
		if (currentIndex == weapons.Count)
		{
			currentIndex = 0;
		}
		currentWeapon = weapons[currentIndex];
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

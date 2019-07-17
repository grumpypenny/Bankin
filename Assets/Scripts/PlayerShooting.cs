using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
	#region Singleton
	public static PlayerShooting instance;

	private void Awake()
	{
		if (instance != null)
		{
			Debug.LogWarning("Multiple Instances of PlayerShooting");
			return;
		}
		instance = this;
	}

	#endregion

	[Header("Inventory References")]
	public List<Weapon> weapons;
	public Weapon currentWeapon;

	[Space]

	[Header("Bullet Asset")]
	public GameObject asset;

	[Space]

	[Header("Physics")]
	public LayerMask hitMask;

	[Space]

	[Header("Graphics")]
	public Transform muzzle;

	private Vector2 direction;
	private float timeOfNextShot = 0;
	private float remainingClip;

	// event that goes off when a bullet is fired
	public event Action<float, float, Vector2, LayerMask> OnPlayerFire;

    // Start is called before the first frame update
    void Start()
    {
		weapons = PlayerInventory.instance.weapons;
		PlayerInventory.instance.weaponChange += UpdateWeapon;
	}

    // Update is called once per frame
    void Update()
    {
		if (Input.GetButtonDown("Fire1") && currentWeapon != null)
		{
			if (Time.time > timeOfNextShot && remainingClip > 0)
			{
				remainingClip--;
				Fire();
				timeOfNextShot = Time.time + currentWeapon.timeBetweenShots;
			}
		}
		if (Input.GetKeyDown(KeyCode.R))
		{
			Reload();
		}
		Debug.DrawRay(muzzle.position, muzzle.right * 100, Color.grey);
    }

	private void Reload()
	{
		remainingClip = currentWeapon.clipSize;
	}

	private void UpdateWeapon()
	{
		currentWeapon = PlayerInventory.instance.currentWeapon;
		remainingClip = currentWeapon.clipSize;
	}

	private void Fire()
	{
		// instanciate fire
		Instantiate(asset, muzzle.position, muzzle.rotation);

		// if event has a listener, invoke it
		// this sets up the bullet to have the correct properties.
		if (OnPlayerFire != null)
		{
			OnPlayerFire.Invoke(currentWeapon.bulletSpeed, currentWeapon.damage, muzzle.right, hitMask);
		}

	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIShooting : MonoBehaviour
{
	[Header("Variables")]
	public Weapon weapon;
	public LayerMask hitMask;

	private AIShootingManager manager;
	[SerializeField] private float timeOfNextShot = 0;
	[SerializeField] private float remainingClip;
	[SerializeField] private float time;
	// Start is called before the first frame update
	void Start()
    {
		manager = AIShootingManager.instance;
		remainingClip = weapon.clipSize;
    }

    // Update is called once per frame
    void Update()
    {
		time = Time.time;
		Look();
		
		if (Time.time > timeOfNextShot && remainingClip > 0)
		{
			timeOfNextShot = Time.time + weapon.timeBetweenShots;
			DebugFire();
		}
		else if (remainingClip <= 0)
		{
			Reload();
		}

	}

	private void Reload()
	{
		print("reloading");
		timeOfNextShot = Time.time + weapon.reloadTime;
		remainingClip = weapon.clipSize;
	}

	// DO NOT USE THIS FOR ACTUAL GAME
	// ADD PATHFINDING AND SIGHT + SOUND SYSTEM
	private void DebugFire()
	{
		remainingClip -= 1;
		Vector2 direction = PlayerShooting.instance.transform.position - transform.position;
		manager.SendFireEvent(transform, weapon.bulletSpeed, weapon.damage, direction, hitMask, weapon.lifeTime);
	}

	private void Look()
	{
		Vector2 direction = PlayerShooting.instance.transform.position - transform.position;
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

		transform.rotation = Quaternion.Euler(0, 0, angle);
	}
}

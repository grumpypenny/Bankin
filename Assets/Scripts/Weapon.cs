using UnityEngine;

/*
 * This Script is for weapon DATA -> not to be set as an object
 */

[CreateAssetMenu(fileName = "New Weapon", menuName = "Items/Weapon")]
public class Weapon : ScriptableObject
{
	[Header("General")]
	new public string name = "Default Name";
	public Sprite sprite;

	[Space]

	[Header("Weapon Specifics")]
	public float damage;
	public float timeBetweenShots;
	public float reloadTime;
	[Range(1, 1000)]
	public int clipSize;
	[Range(1, 1000)]
	public float bulletSpeed;
	public float lifeTime;
	public LayerMask hitMask;
	
}

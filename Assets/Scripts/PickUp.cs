using UnityEngine;

[CreateAssetMenu(fileName = "New Pick Up", menuName = "Items/PickUps")]
public class PickUp : ScriptableObject
{
	[Header("General")]
	new public string name = "Default Pickup";
	public Sprite sprite;

	[Space]

	[Header("Pick Up Specific")]
	public float value;
	public string description = "describe what this pick up is";

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementController))]
public class PlayerMovement : MonoBehaviour
{
	public float speed;
	public GameObject graphics;

	private MovementController movement;
	private Vector2 input;

	private void Start()
	{
		movement = GetComponent<MovementController>();
	}

	// Update is called once per frame
	void Update()
	{
		input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

		Look();
	}

	private void FixedUpdate()
	{
		movement.Move(input.normalized * speed * Time.deltaTime);
	}

	private void Look()
	{
		Vector2 direction = Input.mousePosition - Camera.main.WorldToScreenPoint(graphics.transform.position);
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

		graphics.transform.rotation = Quaternion.Euler(0, 0, angle);
	}

}

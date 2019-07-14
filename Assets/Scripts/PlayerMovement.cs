using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementController))]
public class PlayerMovement : MonoBehaviour
{
	public float speed;

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
	}

	private void FixedUpdate()
	{
		movement.Move(input.normalized * speed * Time.deltaTime);
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This class works by firing rays from the circle collider's
 * surface in the direction of travel
 * 
 * Note: Call move from FIXED UPDATE so that everything works
 */ 

[RequireComponent(typeof(CircleCollider2D))]
public class MovementController : MonoBehaviour
{
	[Header("Mask")]
	public LayerMask CollisionMask;
	public LayerMask selfMask;

	const float skinWidth = 0.1f;

	[Space]

	[Header("Resolution")]
	public int numberOfRays;

	private float raySpacing;
	private CircleCollider2D circle;
	private float radius;

    // Start is called before the first frame update
    void Start()
    {
		circle = GetComponent<CircleCollider2D>();
		radius = circle.radius;
		selfMask = LayerMask.GetMask(LayerMask.LayerToName(gameObject.layer));
		FindRaySpacing();
    }

	// assumes script calling this function already
	// multiplied by Time.deltatime
	public void Move(Vector3 velocity)
	{
		FireRays(ref velocity);

		transform.Translate(velocity);
	}

	private void FireRays(ref Vector3 velocity)
	{
		Debug.DrawRay(transform.position, velocity.normalized * radius, Color.red);

		float rayLength = velocity.magnitude + skinWidth;

		Ray2D travelDirection = new Ray2D(gameObject.transform.position, velocity.normalized);
		Vector3 pointOnCircle = travelDirection.GetPoint(radius - skinWidth);

		Debug.DrawLine(gameObject.transform.position, pointOnCircle, Color.blue);
		Debug.DrawRay(pointOnCircle, Vector2.Perpendicular(velocity.normalized) * radius, Color.red);
		Debug.DrawRay(pointOnCircle, -Vector2.Perpendicular(velocity.normalized) * radius, Color.magenta);

		Ray2D posPerpRay = new Ray2D(pointOnCircle, Vector2.Perpendicular(velocity.normalized));
		for (int j = -1; j <= 1; j+= 2)
		{
			for (int i = j; i < numberOfRays; i++)
			{
				if (i < 0)
				{
					continue;
				}

				Vector2 rayOrigin = pointOnCircle;
				rayOrigin += j * posPerpRay.direction * (raySpacing * i + Vector2.Dot(posPerpRay.direction, velocity));

				RaycastHit2D hit = Physics2D.Raycast(rayOrigin, velocity.normalized, rayLength, CollisionMask);
				Debug.DrawRay(rayOrigin, velocity.normalized * rayLength, Color.cyan);

				if (hit)
				{
					rayLength = hit.distance;
					velocity = velocity * (rayLength - skinWidth);
					print(hit.transform.name);
				}
			}
		}
	}

	// Calculate how far apart the rays should be
	private void FindRaySpacing()
	{
		numberOfRays = Mathf.Clamp(numberOfRays, 2, int.MaxValue);

		raySpacing = circle.radius / (numberOfRays - 1);
	}

}

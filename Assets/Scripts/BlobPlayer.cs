using UnityEngine;
using System;

/*

BlobPlayer is a script that deals with player movement

Written by: Patrick Lathan
(C) 2016

*/
public class BlobPlayer : MonoBehaviour {

	//slope detection code from http://thehiddensignal.com/unity-angle-of-sloped-ground-under-player/

	//TODO: properly implement these constants
	private const float SPEED = 6.0f;
	private const float GRAVITY = -2f;
	private const float JUMPSPEED = 0.5f;
	private const float FRICTION = .5f;

	private float xSpeed;
	private float ySpeed;
	private float zSpeed;

	private Vector3 momentumVector;

	[Header("Results")]
	public float groundSlopeAngle = 0f;            // Angle of the slope in degrees
	public Vector3 groundSlopeDir = Vector3.zero;  // The calculated slope as a vector

	[Header("Settings")]
	public bool showDebug = false;                  // Show debug gizmos and lines
	public LayerMask castingMask;                  // Layer mask for casts. You'll want to ignore the player.
	public float startDistanceFromBottom = 0.5f;   // Should probably be higher than skin width
	public float sphereCastRadius = 0.25f;
	public float sphereCastDistance = 0.5f;       // How far spherecast moves down from origin point

	public float raycastLength = 0.75f;
	public Vector3 rayOriginOffset1 = new Vector3(-0.2f, 0f, 0.16f);
	public Vector3 rayOriginOffset2 = new Vector3(0.2f, 0f, -0.16f);

	private CharacterController charController;

	void Start() {
		charController = GetComponent<CharacterController>();

		xSpeed = 0;
		ySpeed = 0;
		zSpeed = 0;

		momentumVector = Vector3.zero;
	}

	void Update() {
		groundSlopeDir = Vector3.zero;

		if (Input.GetAxis("Horizontal") != 0) {
			xSpeed = Input.GetAxis("Horizontal");
			//Convert local horizontal movement into world movement and add to momentum
			momentumVector += (this.gameObject.transform.right * FRICTION) * xSpeed * Time.deltaTime;
		}


		if (Input.GetAxis("Vertical") != 0) {
			zSpeed = Input.GetAxis("Vertical");
			//Convert local vertical movement into world movement and add to momentum
			momentumVector += (this.gameObject.transform.forward * FRICTION) * zSpeed * Time.deltaTime;
		}

		if (charController.isGrounded) {
			if (Input.GetButtonDown("Jump")) {
				ySpeed = JUMPSPEED;
			} else {
				//TODO: setting yspeed to 0 when grounded is ideal but it appears to make slope movement less smooth
				//ySpeed = 0;
			}
			//if (ySpeed <= GRAVITY) {
			//    Debug.Log("Player impacted ground at maximum speed");
			//}
			//Slope detection needed only if the player is on the ground
			CheckGround(new Vector3(transform.position.x, transform.position.y - (charController.height / 2) + startDistanceFromBottom, transform.position.z));
		} else {
			ySpeed += GRAVITY * Time.deltaTime;
		}

		//TODO: get rid of magic numbers
		//Clamp vector magnitude ("speed") while ignoring movement on y axis
		momentumVector.y = 0;
		momentumVector = Vector3.ClampMagnitude(momentumVector, .75f);

		//Add movement based on slope beneath blob
		momentumVector.x += (groundSlopeDir.x * Time.deltaTime);
		momentumVector.z += (groundSlopeDir.z * Time.deltaTime);

		//Shrink momentum over time
		momentumVector /= 1.05f;

		//Y speed should be unaffected by other calculations
		momentumVector.y = ySpeed;

		if (charController.enabled) {
			charController.Move(momentumVector);
		}

	}

	public void CheckGround(Vector3 origin) {
		// Out hit point from our cast(s)
		RaycastHit hit;

		// SPHERECAST
		// "Casts a sphere along a ray and returns detailed information on what was hit."
		//THESE CONDITIONS ARE NEVER SATISFIED
		if (Physics.SphereCast(origin, sphereCastRadius, Vector3.down, out hit, sphereCastDistance, castingMask)) {
			// Angle of our slope (between these two vectors).
			// A hit normal is at a 90 degree angle from the surface that is collided with (at the point of collision).
			// e.g. On a flat surface, both vectors are facing straight up, so the angle is 0.
			groundSlopeAngle = Vector3.Angle(hit.normal, Vector3.up);

			// Find the vector that represents our slope as well.
			//  temp: basically, finds vector moving across hit surface
			Vector3 temp = Vector3.Cross(hit.normal, Vector3.down);
			//  Now use this vector and the hit normal, to find the other vector moving up and down the hit surface
			groundSlopeDir = Vector3.Cross(temp, hit.normal);
		}

		// Now that's all fine and dandy, but on edges, corners, etc, we get angle values that we don't want.
		// To correct for this, let's do some raycasts. You could do more raycasts, and check for more
		// edge cases here. There are lots of situations that could pop up, so test and see what gives you trouble.
		RaycastHit slopeHit1;
		RaycastHit slopeHit2;

		// FIRST RAYCAST
		if (Physics.Raycast(origin + rayOriginOffset1, Vector3.down, out slopeHit1, raycastLength)) {
			// Debug line to first hit point
			if (showDebug) {
				Debug.DrawLine(origin + rayOriginOffset1, slopeHit1.point, Color.red);
			}
			// Get angle of slope on hit normal
			float angleOne = Vector3.Angle(slopeHit1.normal, Vector3.up);

			// 2ND RAYCAST
			if (Physics.Raycast(origin + rayOriginOffset2, Vector3.down, out slopeHit2, raycastLength)) {
				// Debug line to second hit point
				if (showDebug) {
					Debug.DrawLine(origin + rayOriginOffset2, slopeHit2.point, Color.red);
				}
				// Get angle of slope of these two hit points.
				float angleTwo = Vector3.Angle(slopeHit2.normal, Vector3.up);
				// 3 collision points: Take the MEDIAN by sorting array and grabbing middle.
				float[] tempArray = new float[] { groundSlopeAngle, angleOne, angleTwo };
				Array.Sort(tempArray);
				groundSlopeAngle = tempArray[1];
			} else {
				// 2 collision points (sphere and first raycast): AVERAGE the two
				float average = (groundSlopeAngle + angleOne) / 2;
				groundSlopeAngle = average;
			}
		}
	}

}
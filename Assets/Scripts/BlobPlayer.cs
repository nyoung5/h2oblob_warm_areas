using UnityEngine;
using System;

public class BlobPlayer : MonoBehaviour {

	//@author Nathan Young
	public GameObject waterParticleSystem;
	private ParticleSystem particleSystem;

	//Also see particle system setting: start lifetime
	public const float TIME_ACTIVE = 1f;

	//http://answers.unity3d.com/questions/225213/c-countdown-timer.html
	private float timeLeft; //time left for power on
	private bool canUseWater = true;
	private bool waterIsActive;

	//@author Patrick Lathan
	private const float SPEED = 6.0f;
	private const float JUMPSPEED = 0.5f;
	private const float FRICTION = .5f;
	private const float BASEGRAVITY = -2f;
	private const float VAPORGRAVITY = -1f;
	private const float MOMENTUMDECAY = 1.05f;

	private float gravity;

	private float ySpeed;

	private CharacterController charController;

	public GameObject icePrefab;

	private PowerState currentState;

	private Vector3 momentumVector;

	//http://thehiddensignal.com/unity-angle-of-sloped-ground-under-player/
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

	void Start() {
		//@author Nathan Young
		waterParticleSystem = GameObject.Find("WaterParticleSystem");
		particleSystem = waterParticleSystem.GetComponent<ParticleSystem>();

		waterParticleSystem.SetActive(true);
		particleSystem.Play();
		particleSystem.Stop();

		//@author Patrick Lathan
		charController = GetComponent<CharacterController>();

		//Set intial vertical speed to zero
		ySpeed = 0;

		//Set initial gravity to base value
		gravity = BASEGRAVITY;

		//Set intial momentum to zero
		momentumVector = Vector3.zero;

		//Set initial state
		currentState = new IceState(this);
	}

	//@author Patrick Lathan
	public void setState(PowerState newState) {
		currentState = newState;
	}

	//@author Patrick Lathan
	void Update() {
		//groundSlopeDir should be recalculated every frame
		groundSlopeDir = Vector3.zero;

		if (Input.GetAxis("Horizontal") != 0) {
			float xSpeed = Input.GetAxis("Horizontal");
			//Convert local horizontal movement into world movement and add to momentum
			momentumVector += (this.gameObject.transform.right * FRICTION) * xSpeed * Time.deltaTime;
		}


		if (Input.GetAxis("Vertical") != 0) {
			float zSpeed = Input.GetAxis("Vertical");
			//Convert local vertical movement into world movement and add to momentum
			momentumVector += (this.gameObject.transform.forward * FRICTION) * zSpeed * Time.deltaTime;
		}

		if (!(currentState is VaporState)) {
			// Make sure gravity is always reset when exiting vapor state
			gravity = BASEGRAVITY;
		}

		if (charController.isGrounded) {
			if (Input.GetButtonDown("Jump")) {
				ySpeed = JUMPSPEED;
			}
			//Slope detection needed only if the player is on the ground
			CheckGround(new Vector3(transform.position.x, transform.position.y - (charController.height / 2) + startDistanceFromBottom, transform.position.z));
		}

		ySpeed += gravity * Time.deltaTime;

		//Clamp vector magnitude ("speed") while ignoring movement on y axis
		//Reimplement this in case of uncontrollable acceleration
		//momentumVector.y = 0;
		//momentumVector = Vector3.ClampMagnitude(momentumVector, .75f);

		//Add movement based on slope beneath blob
		momentumVector.x += (groundSlopeDir.x * Time.deltaTime);
		momentumVector.z += (groundSlopeDir.z * Time.deltaTime);

		//Shrink momentum over time
		momentumVector /= MOMENTUMDECAY;

		//Y speed should be unaffected by other calculations
		momentumVector.y = ySpeed;

		if (charController.enabled) {
			charController.Move(momentumVector);
		}

		//Additionally perform Update() for given power state
		currentState.Update();
	}

	//entire CheckGround method from http://thehiddensignal.com/unity-angle-of-sloped-ground-under-player/
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
	}

	//@author Elena Sparacio
	//@author Patrick Lathan
	class IceState : PowerState {
		public IceState(BlobPlayer player) : base(player) {
		}

		public override void Update() {
			if (Input.GetButtonDown("IcePower")) {
				GameObject[] iceCircles = GameObject.FindGameObjectsWithTag("ice");

				//Destroy all previous ice circles when new one is spawned
				foreach (GameObject oldIceCircle in iceCircles) {
					Destroy(oldIceCircle);
				}

				GameObject iceCircle = Instantiate(player.icePrefab) as GameObject;
				GameObject blob = GameObject.Find("ActualBlob");

				Vector3 blobPosition = blob.transform.position + (blob.transform.forward * 2);

				//Place ice in front of blob
				iceCircle.transform.position = blobPosition;
			}
		}
	}

	//@author Patrick Lathan
	class VaporState : PowerState {
		public VaporState(BlobPlayer player) : base(player) {
		}

		public override void Update() {
			if (Input.GetButton("VaporPower")) {
				//Lessen gravity
				player.gravity = VAPORGRAVITY;

				//Allow jumping in midair
				if (Input.GetButtonDown("Jump")) {
					player.ySpeed = JUMPSPEED;
				}
			} else {
				//Reset gravity to normal
				player.gravity = BASEGRAVITY;
			}


		}
	}

	//@author Nathan Young
	class WaterState : PowerState {
		public WaterState(BlobPlayer player) : base(player) {
		}

		public override void Update() {
			//if button pushed and blob in a state where it can use water
			if (Input.GetButton("WaterPower")) {

				player.timeLeft = TIME_ACTIVE;

				//if the water particle system is not already on, turn it on
				if (!player.particleSystem.isPlaying) {
					player.particleSystem.Play();
				}
			} else {
				player.timeLeft -= Time.deltaTime;
				//if player hasn't used water in a while it stops being active
				if (player.timeLeft < 0 && player.particleSystem.isPlaying) {
					player.particleSystem.Stop(); // gameObject.GetComponent<ParticleSystem>().enableEmission = false; maybe use this, but its deprecated
					// above source is from http://answers.unity3d.com/questions/37875/turning-the-particle-system-on-and-off.html
				}
			}
		}
	}
}
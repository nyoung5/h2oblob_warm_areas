using UnityEngine;
using System.Collections;

public class BlobPlayer : MonoBehaviour {

	public float speed = 6.0f;
	public float gravity = -9.8f;
	public float vertSpeed = 0;
	public float jumpSpeed = 100f;

	private CharacterController charController;

	void Start() {
		charController = GetComponent<CharacterController>();
	}

	void Update() {
		float deltaX = Input.GetAxis("Horizontal") * speed;
		float deltaZ = Input.GetAxis("Vertical") * speed;

		if (charController.isGrounded) {
			if (Input.GetButtonDown ("Jump")) {
				vertSpeed = jumpSpeed;
			}
		} else {
			vertSpeed += gravity;
		}


		Vector3 movement = new Vector3(deltaX, vertSpeed, deltaZ);
		movement *= Time.deltaTime;
		movement = transform.TransformDirection(movement);
		charController.Move(movement);
	}


}

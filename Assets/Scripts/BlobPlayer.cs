using UnityEngine;
using System.Collections;

public class BlobPlayer : MonoBehaviour {

	private float speed = 6.0f;
    private float gravity = -1.5f;
    private float vertSpeed = 0;
    private float jumpSpeed = 20f;

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

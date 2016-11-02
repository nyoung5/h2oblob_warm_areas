using UnityEngine;
using System.Collections;

public class BlobPlayer : MonoBehaviour {

	private float speed = 6.0f;
    private float gravity = -9.8f;
    private float jumpSpeed = 10f;
    private float friction = .5f;

    private float xSpeed = 0;
    private float ySpeed = 0;
    private float zSpeed = 0;

    private float horStartTime;
    private float horPeakSpeed;
    private float vertStartTime;
    private float vertPeakSpeed;

    private CharacterController charController;

	void Start() {
		charController = GetComponent<CharacterController>();
        horStartTime = Time.time;
        vertStartTime = Time.time;
    }

	void Update() {
        //float deltaX = Input.GetAxis("Horizontal") * speed;
        //float deltaZ = Input.GetAxis("Vertical") * speed;

        if (Input.GetAxis("Horizontal") != 0) {
            xSpeed += (Input.GetAxis("Horizontal") * friction);
            xSpeed = Mathf.Clamp(xSpeed, -speed, speed);
            horStartTime = Time.time;
            horPeakSpeed = xSpeed;
        } else {
            xSpeed = Mathf.Lerp(horPeakSpeed, 0, ((Time.time - horStartTime) * friction));
        }

        if (Input.GetAxis("Vertical") != 0) {
            zSpeed += (Input.GetAxis("Vertical") * friction);
            zSpeed = Mathf.Clamp(zSpeed, -speed, speed);
            vertStartTime = Time.time;
            vertPeakSpeed = zSpeed;
        } else {
            zSpeed = Mathf.Lerp(vertPeakSpeed, 0, ((Time.time - vertStartTime) * friction));
        }

        if (charController.isGrounded) {
			if (Input.GetButtonDown ("Jump")) {
				ySpeed = jumpSpeed;
			}
		} else {
            ySpeed += gravity * Time.deltaTime;
            //decrease gravity here for floatiness? increase deltatime?
		}


		Vector3 movement = new Vector3(xSpeed, ySpeed, zSpeed);
		movement *= Time.deltaTime;
        //Transform from local to world space
		movement = transform.TransformDirection(movement);
		charController.Move(movement);
	}


}

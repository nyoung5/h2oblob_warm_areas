using UnityEngine;
using System.Collections;

public class BlobPlayer : MonoBehaviour {

	public float speed = 6.0f;
	public float gravity = -9.8f;
	public float vertSpeed;
	public float jumpSpeed = 100f;
	private CharacterController charController;
	private Camera mainCamera;


	// Use this for initialization
	void Start () {
		
		charController = GetComponent<CharacterController>();
		mainCamera = GetComponent<Camera> ();

	}

	// Update is called once per frame
	void Update () {

		float oldX = this.transform.position.x;
		float deltaZ = Input.GetAxis ("Vertical") * speed;
		Vector3 rotation = new Vector3(0.0f, (Input.GetAxis("Horizontal")), 0.0f);


		if (charController.isGrounded) {
			if (Input.GetButtonDown ("Jump")) {
				vertSpeed = jumpSpeed;
			} else {
				vertSpeed = 0;
			}
		}
		else{
			vertSpeed += gravity;
		}
		Vector3 movement = new Vector3(oldX, vertSpeed, deltaZ); 
		movement = Vector3.ClampMagnitude (movement, speed);

		movement *= Time.deltaTime;
		movement = transform.TransformDirection (movement);
		charController.Move (movement);
		this.transform.Rotate(rotation * speed * Time.deltaTime); 


	}


}


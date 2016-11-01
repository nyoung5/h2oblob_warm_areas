using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {
	//
	//    private float speed = 75.0f;
	//
	//    // Use this for initialization
	//    void Start () {
	//    
	//    }

	public enum RotationAxes
	{
		MouseXandY = 0,
		MouseX = 1,
		MouseY = 2
	}
	// Setting for how the mouse should look
	public RotationAxes axes = RotationAxes.MouseXandY; //This will be given to the editor X for player and Y for camera. Question: is this a good idea and will it be a permanent change?

	// General settings - sensitivity
	[SerializeField] private float sensitivityHor = 9.0f;
	[SerializeField] private float sensitivityVert = 9.0f;
	// General settings - y rotation constraints
	[SerializeField] private float maxVert = 45f;
	[SerializeField] private float minVert = -45f;

	// My rotation value.  We have to keep our
	// own, since localEulerAngles must be 0-360.
	// Our variable is kept in range -45 to 45.
	private float rotationX = 0;

	// Update is called once per frame
	void Update () {
		float currentXRot = transform.localEulerAngles.x;
		float currentYRot = transform.localEulerAngles.y;
		float xRot = getXRot ();
		float yRot = getYRot ();
		if (axes == RotationAxes.MouseY)
			yRot = currentYRot;
		if (axes == RotationAxes.MouseX)
			xRot = currentXRot;
		transform.localEulerAngles = new Vector3 (xRot, yRot, 0);
	}

	public float getXRot(){
		float newX = rotationX - Input.GetAxis ("Mouse Y") * sensitivityVert;
		newX = Mathf.Clamp (newX, minVert, maxVert);
		rotationX = newX;
		return newX;
	}

	public float getYRot(){
		float newY = transform.localEulerAngles.y + Input.GetAxis ("Mouse X") * sensitivityHor;
		return newY;
	}

	//    // Update is called once per frame
	//    void Update () {
	//    
	//        //rotate based on arrow keys 
	//        Vector3 vector = new Vector3(0.0f, (Input.GetAxis("Horizontal")), 0.0f);
	//        this.transform.Rotate(vector * speed * Time.deltaTime); 
	//
	//
	//
	//    }
}

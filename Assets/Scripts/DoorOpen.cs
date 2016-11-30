using UnityEngine;
using System.Collections;

/*

DoorOpen is a script attached the to the gate in the game 

Written by: Patrick Lathan and Nathan Young
(C) 2016

*/
public class DoorOpen : MonoBehaviour {

	private float angle;
	private float progress;
	private float startPos;

	// Use this for initialization
	void Start () {
		startPos = transform.eulerAngles.y - 360;
		angle = transform.eulerAngles.y;
		progress = 0;
	}
	
	// Update is called once per frame
	void Update () {

		//always lerp the door until it is closed
		progress -= .005f;
		progress = Mathf.Clamp (progress, 0, 1);
		angle = Mathf.Lerp (startPos, 90, progress);
		this.gameObject.transform.localEulerAngles = new Vector3 (0, angle, 0);
	}

	//This opens the door all the way and restarts the lerp
	public void doorOpenAndClose(){
		angle = 90;
		progress = 1;
	}

}

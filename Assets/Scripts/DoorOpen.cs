using UnityEngine;
using System.Collections;

public class DoorOpen : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void doorOpen(){

		print ("OPEN!");
		this.gameObject.transform.localEulerAngles = new Vector3 (0, 90, 0);

	}
}

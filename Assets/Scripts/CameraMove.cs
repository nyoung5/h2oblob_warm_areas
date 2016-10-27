using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

	private float speed = 75.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		//rotate based on arrow keys 
		Vector3 vector = new Vector3(0.0f, (Input.GetAxis("Horizontal")), 0.0f);
		this.transform.Rotate(vector * speed * Time.deltaTime); 



	}
}

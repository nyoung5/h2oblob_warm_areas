using UnityEngine;
using System.Collections;

public class TestPlayerScript : MonoBehaviour {

    Rigidbody rigidBody;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        
	}
	
	// Update is called once per frame
	void Update () {
        rigidBody.AddForce(0, 0, 2);
    }
}

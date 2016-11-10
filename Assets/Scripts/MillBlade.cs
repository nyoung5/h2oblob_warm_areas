using UnityEngine;
using System.Collections;

public class MillBlade : MonoBehaviour {
	private float speed;

	// Use this for initialization
	void Start () {
		speed = 0;
	}
	
	// Update is called once per frame
	void Update () {
		Mathf.Clamp (speed,0,20f);
		speed *= .95f;
		this.gameObject.transform.Rotate (0,0,speed);
	}

	void OnParticleCollision(GameObject particle){
		
		DoorOpen script = GameObject.Find ("door1").GetComponent<DoorOpen> ();

		if (particle.tag == "water") {
			speed += 1;

			if (speed >= 12) {
				script.doorOpenAndClose ();
			}

		}
	}
}

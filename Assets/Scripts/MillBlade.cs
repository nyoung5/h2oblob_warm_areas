using UnityEngine;
using System.Collections;

public class MillBlade : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.gameObject.transform.Rotate (0,0,20f);
	}

	void OnParticleCollision(GameObject particle){
		
		//DoorOpen script = GameObject.Find ("door1").GetComponent<DoorOpen> ();
		//script.doorOpen ();


		if (particle.tag == "water") {
			print ("water");
			//this.gameObject.transform.Rotate (20f,20f,20f);
		
		}
	}
}

using UnityEngine;
using System.Collections;

/*

Blobrarian is the script that is attached to the Blobrarian

Written by: Elena Sparacio 
(C) 2016

*/
public class Blobrarian : MonoBehaviour {

	private bool cutscene;

	// Use this for initialization
	void Start () {

		//the cutscene has not been played upon initialization
		cutscene = false; 

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//setCutscene is called to the set whether or not the first cutscene has been played
	public void setCutscene(bool isTrue){
		cutscene = isTrue;
	}

	//Play certain animations
	void OnTriggerEnter(Collider other){

		if (cutscene) {

			//get secondary camera
			Camera movieCam = GameObject.Find ("SecondaryCamera").GetComponent<Camera> ();

			//set location? 
			movieCam.transform.position = new Vector3 (-59, 5, -50);

			//set camera view
			MovieCamera cameraScript = GameObject.Find ("SecondaryCamera").GetComponent<MovieCamera> ();
			cameraScript.SetMovieCam ();
			cameraScript.LibraryCutscene ();

		} else {

			//Print generic dialogue
			GameObject UICanvas = GameObject.Find ("UICanvas");
			UI uiScript = UICanvas.GetComponent<UI>();
			uiScript.Dialog ("Good luck saving your Grandblob.");

		}


	}
}

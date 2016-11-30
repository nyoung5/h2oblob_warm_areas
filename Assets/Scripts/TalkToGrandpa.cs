using UnityEngine;
using System.Collections;

/*

TalkToGranpa is the script that is attached to Grandblob

Written by: Elena Sparacio and Patrick Lathan
(C) 2016

*/
public class TalkToGrandpa : MonoBehaviour {

	public float spawnTime = 0.5f;
	const float MIN_DIST = 5.0f;
	private bool cutscene1; 

	// Use this for initialization
	void Start () {

		//the cutscene has not been played upon initialization
		cutscene1 = false; 
		InvokeRepeating ("DetectPlayer", spawnTime, spawnTime);
	
	}

	// Update is called once per frame
	void Update () {

	}

	//setCutscene is called to the set whether or not the first cutscene has been played
	public void setCutscene(bool isTrue){
		cutscene1 = isTrue;
	}

	//Play certain animations
	void OnTriggerEnter(Collider other){

		if (cutscene1) {

			//get secondary camera
			Camera movieCam = GameObject.Find ("SecondaryCamera").GetComponent<Camera> ();

			//set location
			movieCam.transform.position = new Vector3 (86, 5, -34);

			//set camera view
			MovieCamera cameraScript = GameObject.Find ("SecondaryCamera").GetComponent<MovieCamera> ();
			cameraScript.SetMovieCam ();
			cameraScript.grandpaCutscene ();

		} else {

			//GrandBlob dialogue
			GameObject UICanvas = GameObject.Find ("UICanvas");
			UI uiScript = UICanvas.GetComponent<UI>();
			uiScript.Dialog ("*shiver shiver*");

		}
	

	}

	//This checks if the player is close to grandblob, and if the player uses ice powers, 
	//it kills grandblob :( 
	void DetectPlayer(){

		//if ice is too close to him
		GameObject [] iceMagics = GameObject.FindGameObjectsWithTag ("ice");
		for(var i = 0 ; i < iceMagics.Length ; i ++)
		{
			Vector3 icePlace = iceMagics [i].transform.position;
			if (Vector3.Distance (transform.position, icePlace) <= MIN_DIST) {
				GameObject UICanvas = GameObject.Find ("UICanvas");
				UI uiScript = UICanvas.GetComponent<UI>();
				uiScript.Dialog ("It is too cold! Oh noooo!");
			}
		}

	}
}
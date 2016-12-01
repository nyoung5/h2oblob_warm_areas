using UnityEngine;
using System.Collections;

/*

PlantSunFlower is the script attached to the player that allows it to plant 
seeds.

Written by: Nathan Young and Elena Sparacio
(C) 2016

*/
public class PlantSunFlower : MonoBehaviour {

	//variables including default number of seeds
	public GameObject seedPrefab;
	private int numSeeds;
	private bool isFirst;


	// Use this for initialization
	void Start () {

		numSeeds = 0;
		isFirst = true;
	
	}
	
	// Update is called once per frame
	void Update () {
	
		//if the player is allowed to plant a seed, put it down in front of the player
		if(Input.GetButtonDown("PlantSunFlower") && numSeeds>0)
		{
			GameObject seed = Instantiate (seedPrefab) as GameObject;
			GameObject blob = GameObject.Find ("ActualBlob");

			Vector3 blobPosition = blob.transform.position + (blob.transform.forward*2);
			seed.transform.position = blobPosition;
			numSeeds--;

		}

	}

	//GotASeed is called when the player picks up a seed. It increments the amount of seeds
	//they currently have. If it is the first collected seed, it plays instructions
	public void GotASeed(){
		numSeeds++;
		if (isFirst) {

			MovieCamera movieScript = GameObject.Find ("SecondaryCamera").GetComponent<MovieCamera> ();
			movieScript.SeedInstructions ();
			isFirst = false;
		}
	}
}

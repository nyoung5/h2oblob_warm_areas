using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/*

Seed is the script that is attached to a seed in the game.

Written by: Elena Sparacio
(C) 2016

*/
public class Seed : MonoBehaviour {

	private bool isFirst;

	// Use this for initialization
	void Start () {

		isFirst = true;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//if the player hits the seed, allow it to be collected
	void OnTriggerEnter(Collider other) {

		if (other.tag == "Player") {
			SeedCollected ();
			Destroy(this.gameObject);
		}
	}

	//seedCollected is called when the player gets a new seed. 
	void SeedCollected(){

		//tell the player they got a seed
		GameObject UICanvas = GameObject.Find ("UICanvas");
		UI uiScript = UICanvas.GetComponent<UI>();

		if (isFirst) {
			
			uiScript.setSkip (false);

			string[] messages = new string[4];
			messages [0] = "You can plant seeds by pressing 'shift' to gain more powers.";
			messages [1] = "One planted seed will give you water powers in an area.";
			messages [2] = "Two planted seeds will give you vapor powers in an area.";
			messages [3] = "Once you collect all the seeds, plant them around Grandblob to warm him.";

			//create times and colors arrays
			int[] times = new int[4];
			for (int i = 0; i < times.Length; i++) {
				times [i] = 3;
			}
			Color[] colors = new Color[4];
			for (int i = 0; i < colors.Length; i++) {
				colors [i] = Color.white;
			}
						
			StartCoroutine (uiScript.specialWait (messages, times, colors));
			isFirst = false;
		} else {

			uiScript.PrintCenterMessage ("Congrats! You collected a seed!", 5);

		}

		//add a seed to the seed counter in another script
		PlantSunFlower script = GameObject.Find ("ActualBlob").GetComponent<PlantSunFlower> ();
		script.GotASeed ();

	}
		

}

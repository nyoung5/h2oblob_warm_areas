using UnityEngine;
using System.Collections;

public class StateTest : MonoBehaviour {

	string [] states;
	int currentPlace;

	// Use this for initialization
	void Start () {
		
		states = new string[3];
		states [0] = "ice";
		states [1] = "water";
		states [2] = "vapor";
		currentPlace = 0;
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetButtonDown ("StateChange")) {
			//debug
			print ("State change!");
			GameObject player = GameObject.FindGameObjectsWithTag ("Player") [0];
			currentPlace++;
			if (currentPlace > 2) {
				currentPlace = 0;
			} 
			string nextState = states [currentPlace];
			player.GetComponent<BlobPlayer> ().SetState (nextState);
		}
	}
}

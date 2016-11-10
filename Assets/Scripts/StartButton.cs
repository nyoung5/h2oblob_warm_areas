using UnityEngine;
using UnityEditor;
using System.Collections;

public class StartButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void startGame(){

		Application.LoadLevel("SnowMountains");

	}

	public void quitGame(){

		print ("Bye bye");

		if (EditorApplication.isPlaying = true) {
			EditorApplication.isPlaying = false;
		} else {
			Application.Quit ();
		}


	}
}

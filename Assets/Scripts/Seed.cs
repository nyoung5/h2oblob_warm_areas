using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Seed : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {

		if (other.tag == "Player") {
			SeedCollected ();
			Destroy(this.gameObject);
		}
	}

	void SeedCollected(){

		GameObject UICanvas = GameObject.Find ("UICanvas");
		UI uiScript = UICanvas.GetComponent<UI>();
		uiScript.PrintCenterMessage ("Congrats! You collected a seed!", 5);
		uiScript.updateScore ();

	}
		

}

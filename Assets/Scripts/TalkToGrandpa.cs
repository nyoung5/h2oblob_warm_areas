using UnityEngine;
using System.Collections;

public class TalkToGrandpa : MonoBehaviour {

	public float spawnTime = 0.5f;
	const float MIN_DIST = 5.0f;
	private bool isSpoken;
	GameObject player;


	// Use this for initialization
	void Start () {

		InvokeRepeating ("DetectPlayer", spawnTime, spawnTime);
		player = GameObject.Find ("ActualBlob");
		isSpoken = false;


	}

	// Update is called once per frame
	void Update () {

	}

	void DetectPlayer(){

		Vector3 playerLocation = player.transform.position;
		Vector3 grandpaLocation = this.gameObject.transform.position;

		//on first talk
		if (Vector3.Distance(transform.position, playerLocation) <= MIN_DIST){
			if(!(isSpoken)){
				GameObject UICanvas = GameObject.Find ("UICanvas");
				UI uiScript = UICanvas.GetComponent<UI>();
				uiScript.hiGrandBlob ("Oh hello there. It's me, your GrandBlob.");
				isSpoken = true;
			}
		}

		//if ice is too close to him
		GameObject [] iceMagics = GameObject.FindGameObjectsWithTag ("ice");
		for(var i = 0 ; i < iceMagics.Length ; i ++)
		{
			Vector3 icePlace = iceMagics [i].transform.position;
			if (Vector3.Distance (transform.position, icePlace) <= MIN_DIST) {
				GameObject UICanvas = GameObject.Find ("UICanvas");
				UI uiScript = UICanvas.GetComponent<UI>();
				uiScript.hiGrandBlob ("It is too cold! Oh noooo!");
			}
		}

	}
}
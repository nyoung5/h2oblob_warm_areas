using UnityEngine;
using System.Collections;

public class TalkToGrandpa : MonoBehaviour {

	public float spawnTime = 0.2f;
	const float MIN_DIST = 5.0f;
	GameObject player;


	// Use this for initialization
	void Start () {

		//InvokeRepeating ("DetectPlayer", spawnTime, spawnTime);
		player = GameObject.Find ("ActualBlob");


	}

	// Update is called once per frame
	void Update () {

		DetectPlayer ();

	}

	void DetectPlayer(){

		Vector3 playerLocation = player.transform.position;
		Vector3 grandpaLocation = this.gameObject.transform.position;

		if (Vector3.Distance(transform.position, playerLocation) <= MIN_DIST){
			print ("Hello I am grandpa");
		}


	}
}
using UnityEngine;
using System.Collections;


//@author Nathan Young
public class PlantSunFlower : MonoBehaviour {
	public GameObject seedPrefab;

	private int numSeeds = 5;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if(Input.GetButtonDown("PlantSunFlower") && numSeeds>0)
		{

			GameObject seed = Instantiate (seedPrefab) as GameObject;
			GameObject blob = GameObject.Find ("ActualBlob");

			Vector3 blobPosition = blob.transform.position + (blob.transform.forward*2);
			//Vector3 blobForward = blob.transform.forward;

			seed.transform.position = blobPosition;
			numSeeds--;

		}

	}
}

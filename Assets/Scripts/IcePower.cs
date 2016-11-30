using UnityEngine;
using System.Collections;

/*

IcePower allows the player to use their ice powers

Written by: Elena Sparacio, Patrick Lathan, and Nathan Young
(C) 2016

*/

public class IcePower : MonoBehaviour {

	public GameObject icePrefab;
	private bool isIce;

	// Use this for initialization
	void Start () {
		isIce = true;
	}

	// Update is called once per frame
	void Update () {

		//if the user uses their ice power
		if(Input.GetButtonDown("IcePower") && (isIce))
		{
			//destroy all other ice circles
			GameObject[] iceCircles = GameObject.FindGameObjectsWithTag("ice");
			foreach (GameObject oldIceCircle in iceCircles) {
				Destroy(oldIceCircle);
			}

			//create a new ice circle in front of the blob
			GameObject iceCircle = Instantiate (icePrefab) as GameObject;
			GameObject blob = GameObject.Find ("ActualBlob");
			Vector3 blobPosition = blob.transform.position + (blob.transform.forward*2);
			iceCircle.transform.position = blobPosition;

		}

	}

	//enableIce can be called to enable or disable ice powers depending upon
	//temperature 
	public void enableIce(bool isEnabled){
		isIce = isEnabled;
	}


}

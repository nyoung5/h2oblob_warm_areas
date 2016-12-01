using UnityEngine;
using System.Collections;

//@author Nathan Young
public class WarmArea : MonoBehaviour {

	BlobPlayer blobPlayer;

	// Use this for initialization
	void Start () {
		GameObject actualBlob = GameObject.Find ("ActualBlob");
		blobPlayer = actualBlob.GetComponent<BlobPlayer> ();
	}

	// Update is called once per frame
	void Update () {

	}

// 	void OnTriggerEnter(Collider other){
// 		if (other.tag == "Player") {
// 			Debug.Log ("inside the circle");
// 			string currentState = blobPlayer.GetState().ToString();
// 			if(currentState == "BlobPlayer+IceState"){
// 				Debug.Log ("Icey State");
// 			}
// 			else if(currentState== "BlobPlayer+WaterState"){
// 				Debug.Log("Watery State");
// 			}else{
// 				Debug.Log("Vaporious State");
// 			}
//
// 		}
// 	}
//
// //note, when destroyed, must say outside circle
// 	void OnTriggerExit(Collider other){
// 		if (other.tag == "Player") {
// 			Debug.Log ("outside the circle");
//
// 		}
// 	}

void OnDestroy(){}


}

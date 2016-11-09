using UnityEngine;
using System.Collections;

public class IcePower : MonoBehaviour {

	public GameObject icePrefab;


	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {


		if(Input.GetButtonDown("IcePower"))
		{
			GameObject[] iceCircles = GameObject.FindGameObjectsWithTag("ice");

			foreach (GameObject oldIceCircle in iceCircles) {
				Destroy(oldIceCircle);
			}

			GameObject iceCircle = Instantiate (icePrefab) as GameObject;
			GameObject blob = GameObject.Find ("ActualBlob");

			Vector3 blobPosition = blob.transform.position + (blob.transform.forward*2);

			iceCircle.transform.position = blobPosition;

		}

	}


}

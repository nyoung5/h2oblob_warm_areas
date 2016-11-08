using UnityEngine;
using System.Collections;

public class Water : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {

		if (other.tag == "Player") {

			StartCoroutine (YouDrowned ());

		}
	}
		

	public IEnumerator YouDrowned() {
		
		yield return new WaitForSeconds(1);
		GameObject blob = GameObject.Find ("NewBlob");
		MeshRenderer renderer = blob.GetComponent<MeshRenderer>();
		renderer.enabled=false;

		GameObject UICanvas = GameObject.Find ("UICanvas");
		UI uiScript = UICanvas.GetComponent<UI>();
		uiScript.YouDied ();

	}
}

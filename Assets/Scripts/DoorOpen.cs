using UnityEngine;
using System.Collections;

public class DoorOpen : MonoBehaviour {

	private float angle;
	private float progress;
	private float startPos;

	// Use this for initialization
	void Start () {
		startPos = transform.eulerAngles.y;
		angle = transform.eulerAngles.y;
		progress = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
		progress -= .001f;
		progress = Mathf.Clamp (progress, 0, 1);
		angle = Mathf.Lerp (startPos, 90, progress);
		this.gameObject.transform.localEulerAngles = new Vector3 (0, angle, 0);

	
	}

	public void doorOpenAndClose(){
		
		angle = 90;
		progress = 1;
	
	}


}

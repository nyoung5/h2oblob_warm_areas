using UnityEngine;
using System.Collections;

public class MovieCamera : MonoBehaviour {

	[SerializeField] Camera movieCam;
	[SerializeField] Camera charCam;

	private float speed = 0.13f;
	private Vector3 destination;
	private bool isBegin;

	// Use this for initialization
	void Start () {

		SetMovieCam ();
		CutsceneSetup ();

		//destination is just before we meet our character
		destination = new Vector3 (34, 4, -30);
		isBegin = true;

		//begin cutscene
		startAnimation ();
	
	}
	
	// Update is called once per frame
	void Update () {
		if (isBegin) {
			movieCam.transform.position = Vector3.Lerp (transform.position, destination, speed * Time.deltaTime);
		}

		//skip animation if enter/return key is pressed
		if (Input.GetKeyDown("enter")||(Input.GetKeyDown("return")))
		{
			print ("Skip cutscenes");
			OnTriggerEnter (null);
		}
	}

	void OnTriggerEnter(Collider other) {

		if (isBegin) {
			//set character camera to allow the player to control the blob
			SetCharCam ();
			//move movie camera out of view
			movieCam.transform.position = new Vector3 (movieCam.transform.position.x, movieCam.transform.position.y - 40, movieCam.transform.position.z);
		} else {
			print ("Entered the secondary camera trigger area");
		}

	}

	void CutsceneSetup(){

		//since this is run on initilization, set the cutscene to be fresh
		TalkToGrandpa script = GameObject.Find ("grandpa").GetComponent<TalkToGrandpa> ();
		script.setCutscene (true);

	}

	public void SetCharCam(){

		isBegin = false;

		//unlock cursor
		Cursor.visible = true;

		//change camera appropriately
		movieCam.enabled = false;
		charCam.enabled = true; 

		//take off movie bars
		setMovieScreen(false);


	}

	public void setMovieScreen(bool isEnabled){
		
		//get movie canvas
		Canvas movieCanvas = GameObject.Find ("MovieScreen").GetComponent<Canvas>();
		movieCanvas.enabled = isEnabled;

	}

	public void SetMovieCam(){

		//lock cursor
		Cursor.visible = false;

		//change camera appropriately
		movieCam.enabled = true;
		charCam.enabled = false; 

		setMovieScreen (true);

	}

	public void startAnimation(){

		Cursor.visible = false;

		GameObject UICanvas = GameObject.Find ("UICanvas");
		UI uiScript = UICanvas.GetComponent<UI>();

		//Print introduction
		string [] messages = new string[8];
		messages [0] = "You begin in the Arctic Tundra...";
		messages [1] = "A blob with no name that has the ability to control ice.";
		messages [2] = "Until now, you’ve just used your abilities to go ice-skating";
		messages [3] = "and freeze ground beef for those tacos you’d like to make next month.";
		messages [4] = "But one day...";
		messages [5] = "Everything changes when you go to visit your Grandblob.";
		messages [6] = "";
		messages [7] = "Go find your Grandblob.";

		int[] times = new int[8];
		times [0] = 5;
		times [1] = 5;
		times [2] = 5;
		times [3] = 5;
		times [4] = 5;
		times [5] = 5;
		times [6] = 5;
		times [7] = 5;

		StartCoroutine (uiScript.specialWait (messages, times));

	}
}

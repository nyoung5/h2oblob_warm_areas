using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI : MonoBehaviour {

	GameObject centerMessage;
	Text centerText;
	private bool isSkip;

	public float uiBaseScreenHeight = 1000f;


	// Use this for initialization
	void Start () {

		centerMessage = GameObject.Find ("CenterMessage");
		centerText = centerMessage.GetComponent<Text>();
		centerText.fontSize = GetScaledFontSize(100);
		isSkip = false;
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown ("c")) {
			print ("Pressing c");
			StartCoroutine (showControls (5));
		}
	
	}

	//set dynamic font size
	//Adapted from: http://gamedev.stackexchange.com/questions/114452/unity-ui-text-font-size-changes-with-the-screen-resolution-problem
	private int GetScaledFontSize (int baseFontSize) {
		float uiScale = Screen.height / uiBaseScreenHeight;
		int scaledFontSize = Mathf.RoundToInt(baseFontSize * uiScale);
		return scaledFontSize;
	}

	public void setSkip(bool isTrue){
		isSkip = isTrue;
	}

	public void PrintCenterMessage(string aMessage, int aTime){

		centerText.text = aMessage;
	    StartCoroutine (Wait (aTime));

	}

	public IEnumerator specialWait(string [] messages, int [] times, Color [] colors){
	
		for (var i = 0; i < messages.Length; i++) {
			if (isSkip) {
				break;
			}
			centerText.color = colors [i];
			centerText.text = messages [i];
			int waitTime = times [i];
			yield return new WaitForSeconds (waitTime);
		}

		centerText.text = "";
		MovieCamera script = GameObject.Find ("SecondaryCamera").GetComponent<MovieCamera> ();
		script.SetCharCam ();

	}

	public void YouDied(){

		centerText.text = "...you... Died...";
		Cursor.lockState = CursorLockMode.None;
		Time.timeScale = 0;

	}

	public void Dialog(string message){

		centerText.text = "\n\n\n\n\""+message+"\"";

		if (message == "It is too cold! Oh noooo!") {
			StartCoroutine(GrandBlobDie ());
		} else {
			StartCoroutine (Wait (5));
		}

	}

	public IEnumerator GrandBlobDie(){

		yield return new WaitForSeconds(3);
		GameObject grandblob = GameObject.Find ("grandpa");
		Destroy (grandblob.gameObject);

	}

	public IEnumerator Wait(int aTime) {
		yield return new WaitForSeconds(aTime);
		centerText.text = "";

	}

	public IEnumerator showControls(int aTime) {

		print ("show controls");

		GameObject uiCanvas = GameObject.Find ("UICanvas");
		Text uiText = uiCanvas.GetComponent<Text> ();
		uiText.text = "~~~CONTROLS~~~ \n Use w, a, s, d to move.\n" +
			"Use the mouse to change where you are looking. \n " +
			"Press down on the mouse to use your ice abilities.\n " +
			"Press the key 'q' to use your water powers.";
		yield return new WaitForSeconds(aTime);
		uiText.text = "";

	}

	public void YouWin(){

		centerText.text = "You found all the seeds! You have saved GrandBlob!";
		Cursor.lockState = CursorLockMode.None;
		Time.timeScale = 0;

	}

}

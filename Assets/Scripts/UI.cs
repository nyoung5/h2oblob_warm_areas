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

//	public void updateScore(){
//
//		GameObject scoreCanvas = GameObject.Find ("Score");
//		Text textObject = scoreCanvas.GetComponent<Text>();
//		int score = int.Parse (textObject.text);
//		score = score + 1; 
//		if (score == 3) {
//			YouWin ();
//		}
//		textObject.text = score.ToString();
//
//	}

	public void YouWin(){

		centerText.text = "You found all the seeds! You have saved GrandBlob!";
		Cursor.lockState = CursorLockMode.None;
		Time.timeScale = 0;

	}

}

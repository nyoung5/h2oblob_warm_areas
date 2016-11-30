using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/*

UI is the script that is attached to the user interface canvas. It contains 
methods to alter this canvas.

Written by: Elena Sparacio
(C) 2016

*/

public class UI : MonoBehaviour {

	//Variables to hold the centerMessage text
	GameObject centerMessage;
	Text centerText;
	//bool that is true if the user chose to skip the cutscene
	private bool isSkip;
	private bool isEnd;



	// Use this for initialization - sets the center message game object
	void Start () {

		centerMessage = GameObject.Find ("CenterMessage");
		centerText = centerMessage.GetComponent<Text>();
		isSkip = false;
		isEnd = false;
		
	}
	
	// Update is called once per frame
	void Update () {

		//if the key "c" is pressed, show the controls
		if (Input.GetKeyDown ("c")) {
			print ("Pressing c");
			StartCoroutine (showControls (5));
		}
	
	}

	//setSkip is a method that sets the isSkip bool. If set to true,
	//this will stop the cutscene
	public void setSkip(bool isTrue){
		isSkip = isTrue;
	}

	//At the end of the game, this boolean is changed to true! :) 
	public void IsEnd(bool isTrue){
		isEnd = isTrue;
	}

	//Print center message will display a string on the screen for a certain
	//amount of time
	public void PrintCenterMessage(string aMessage, int aTime){
		centerText.text = aMessage;
	    StartCoroutine (Wait (aTime));
	}

	//This method takes 3 arrays and goes through them to display a full conversation 
	//between blobs. It changes color and display time based upon what is set in the array.

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

		//switch back to normal view 
		centerText.text = "";
		MovieCamera script = GameObject.Find ("SecondaryCamera").GetComponent<MovieCamera> ();
		script.SetCharCam ();

		//if the game is over, load the main menu
		if (isEnd) {
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.Confined;
			Application.LoadLevel("MainMenu");
		}

	}

	//You Died activates if the player dies. It displays a message and does not let the player move
	public void YouDied(){

		centerText.text = "...you... Died...";
		Cursor.lockState = CursorLockMode.None;
		Time.timeScale = 0;

	}

	//Dialog is a method that displays a string at the lower part of the screen
	public void Dialog(string message){

		centerText.text = "\n\n\n\n\""+message+"\"";

		//if you killed Grandblob (wow you monster) 
		if (message == "It is too cold! Oh noooo!") {
			StartCoroutine(GrandBlobDie ());
		} else {
			StartCoroutine (Wait (5));
		}

	}

	//GrandBlobDie is activated if you try to freeze your Grandblob to death 
	public IEnumerator GrandBlobDie(){

		yield return new WaitForSeconds(3);
		GameObject grandblob = GameObject.Find ("grandpa");
		Destroy (grandblob.gameObject);

	}

	//Waits a certain number of seconds and then changes the center message to blank 
	public IEnumerator Wait(int aTime) {
		yield return new WaitForSeconds(aTime);
		centerText.text = "";

	}

	//showControls will create a small popup on the bottom of the window that tells the player
	//what the controls are 
	public IEnumerator showControls(int aTime) {

		print ("show controls");

		GameObject uiCanvas = GameObject.Find ("UICanvas");
		Text uiText = uiCanvas.GetComponent<Text> ();
		uiText.text = "~~~CONTROLS~~~ \n Use w, a, s, d to move.\n" +
			"Press space to jump.\n" + 
			"Use the mouse to change where you are looking. \n " +
			"Press down on the mouse to use your ice abilities.\n " +
			"Press the key 'q' to use your water powers.\n" +
			"Press the key 'e' to use your vapor powers.\n";
		yield return new WaitForSeconds(aTime);
		uiText.text = "";

	}

	//you Win activates once you have finished the game - this will probably change
	public void YouWin(){

		centerText.text = "You found all the seeds! You have saved GrandBlob!";
		Cursor.lockState = CursorLockMode.None;
		Time.timeScale = 0;

	}

}

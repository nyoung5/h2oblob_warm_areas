using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI : MonoBehaviour {

	// Use this for initialization
	void Start () {

		Instructions ();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Instructions(){

		GameObject centerMessage = GameObject.Find ("CenterMessage");
		Text textObject = centerMessage.GetComponent<Text>();
		textObject.text = "There are 3 magic plants around this area. Find them all!";
		StartCoroutine (Wait (textObject));


	}

	public void YouDied(){
		
		GameObject centerMessage = GameObject.Find ("CenterMessage");
		Text textObject = centerMessage.GetComponent<Text>();
		textObject.text = "...you... Died...";
		Cursor.lockState = CursorLockMode.None;
		Time.timeScale = 0;


	}

	public void hiGrandBlob(string message){

		GameObject centerMessage = GameObject.Find ("CenterMessage");
		Text textObject = centerMessage.GetComponent<Text>();
		textObject.text = "\n\n\n\n\""+message+"\"";

		if (message == "It is too cold! Oh noooo!") {
			StartCoroutine(GrandBlobDie ());
		} else {
			StartCoroutine (Wait (textObject));
		}

	}

	public IEnumerator GrandBlobDie(){

		yield return new WaitForSeconds(3);
		GameObject grandblob = GameObject.Find ("grandpa");
		Destroy (grandblob.gameObject);

	}

	public void SeedCollected(){

		GameObject centerMessage = GameObject.Find ("CenterMessage");
		Text textObject = centerMessage.GetComponent<Text>();
		textObject.text = "Congrats! You collected a magic seed!";
		StartCoroutine (Wait (textObject));

	}

	public IEnumerator Wait(Text aText) {
		yield return new WaitForSeconds(3);
		aText.text = "";

	}

	public void updateScore(){

		GameObject scoreCanvas = GameObject.Find ("Score");
		Text textObject = scoreCanvas.GetComponent<Text>();
		int score = int.Parse (textObject.text);
		score = score + 1; 
		textObject.text = score.ToString();

	}

}

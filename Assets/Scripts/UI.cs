using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void YouDied(){
		
		GameObject centerMessage = GameObject.Find ("CenterMessage");
		Text textObject = centerMessage.GetComponent<Text>();
		textObject.text = "...you... Died...";
		Cursor.lockState = CursorLockMode.None;
		Time.timeScale = 0;


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

}

using UnityEngine;
using System.Collections;

public class TownBlob : MonoBehaviour {

	private string [] genericDialogue;
	private static int MIN = 0;
	private static int MAX = 9;

	// Use this for initialization
	void Start () {

		//initialize generic dialogue
		genericDialogue = new string[9];
		genericDialogue [0] = "Hello!";
		genericDialogue [1] = "It's such a wonderful day.";
		genericDialogue [2] = "Blub blub blub";
		genericDialogue [3] = "Good to see you!";
		genericDialogue [4] = "I'm thinking of adopting a snowman...";
		genericDialogue [5] = "BLUB BLUB";
		genericDialogue [6] = "Have you considered investment banking?";
		genericDialogue [7] = "I think Ted Cruz is the zodiac killer...";
		genericDialogue [8] = "Excuse me, do you have a moment to talk about our lord and savior, Blobman?";
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {

		System.Random rand = new System.Random ();
		int random = rand.Next (MIN, MAX);
		GameObject UICanvas = GameObject.Find ("UICanvas");
		UI uiScript = UICanvas.GetComponent<UI>();
		uiScript.Dialog (genericDialogue[random]);

	}

}

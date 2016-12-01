using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Temperature : MonoBehaviour {

	// Image: https://pixabay.com/p-159386/?no_redirect with photoshop edits
	public Slider tempSlider;


	// Use this for initialization
	void Start () {

		tempSlider = GameObject.Find ("Slider").GetComponent<Slider>();

	}

	// Update is called once per frame
	void Update () {

	}

	//method accessible by all scripts that will allow you to change the temperature bar
	//int temp can be an integer from 1 - 3
	public void setTemp(float temp){

		tempSlider.value = temp;

	}
	public float GetTemp(){
		return tempSlider.value;
	}
}
